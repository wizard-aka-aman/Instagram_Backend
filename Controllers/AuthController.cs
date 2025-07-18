using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Instagram.Model;
using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Instagram.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly InstagramContext _societyContext;
        private readonly IConfiguration configuration;
        public AuthController(InstagramContext societyContext, IConfiguration config)
        {
            _societyContext = societyContext;
            configuration = config;
        }

        [HttpPost("login/")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto)
        {

            Users user = await _societyContext.Users.FirstOrDefaultAsync(e => e.Email == dto.email);
            if (user == null)
            {
                //return Ok("User Not Registered!");
                return NotFound("User Not Registered!");
            }
            UsersDto userDto = new UsersDto
            {
                Bio = user.Bio,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                UsersId = user.UsersId,
                ProfilePicture = user.ProfilePicture != null ?  Convert.ToBase64String(user.ProfilePicture) : null
            };

            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub ,configuration["JwtConfig:Subject"]?? "" ),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString() ),
                new Claim("UserName" , user.UserName.ToString() ?? ""),
                new Claim("Email" , user.Email.ToString()?? "" ),
                new Claim("FullName" , user.FullName.ToString()?? ""), 
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

            return Ok(new { Token = tokenValue, User = userDto });

        }
    }
}
