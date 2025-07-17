using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Instagram.Model;
using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly InstagramContext _context;
        private readonly IConfiguration configuration;
        public AccountController(InstagramContext context, IConfiguration config)
        {
            _context = context;
            configuration = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new {message = ModelState });

            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest(new { message = "Email already exists" });
            if (await _context.Users.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest(new { message = "UserName already exists" });

            var hashedPassword = HashPassword(dto.Password);

            var user = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub ,configuration["JwtConfig:Subject"]?? "" ),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString() ),
                new Claim("UserName" , user.UserName.ToString() ?? ""),
                new Claim("Email" , user.Email.ToString()?? "" ),
                new Claim("FullName" , user.FullName.ToString()?? "" ),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["JwtConfig:Issuer"],
                configuration["JwtConfig:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signIn
                );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new {message = "User registered successfully" , token = tokenValue });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = ModelState });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if(user == null)
            return Unauthorized(new { message = "User not Login" });

            if ( !VerifyPassword(user.PasswordHash, dto.Password))
                return Unauthorized(new { message = "Invalid credentials" });

            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub ,configuration["JwtConfig:Subject"]?? "" ),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString() ),
                new Claim("UserName" , user.UserName.ToString() ?? ""),
                new Claim("Email" , user.Email.ToString()?? "" ),
                new Claim("FullName" , user.FullName.ToString()?? "" ),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["JwtConfig:Issuer"],
                configuration["JwtConfig:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signIn
                );
            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new { message = "Login successful" , token = tokenValue });
        }
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public static bool VerifyPassword(string hashedPasswordWithSalt, string providedPassword)
        {
            var parts = hashedPasswordWithSalt.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed == parts[1];
        }

    }
}
