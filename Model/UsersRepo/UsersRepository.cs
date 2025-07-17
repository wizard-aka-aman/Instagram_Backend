using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.UsersRepo
{
    public class UsersRepository : IUsersRepository
    {
        private readonly InstagramContext _context;

        public UsersRepository(InstagramContext context)
        {
            _context = context;
        }

        public async Task<UsersDto?> EditUsersByUserName(string username, UsersDto dto)
        {
            Users users = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (users == null)
            {
                return null;
            }
            else
            {
                users.Bio = dto.Bio;
                users.Email = dto.Email;
                users.FullName = dto.FullName;
                users.UserName = dto.UserName;
                users.Gender = dto.Gender;
                _context.Users.Update(users);
                await _context.SaveChangesAsync();
                UsersDto updatedUserDto = new UsersDto
                {
                    Bio = users.Bio,
                    Email = users.Email,
                    FullName = users.FullName,
                    UserName = users.UserName,
                    UsersId = users.UsersId,
                    Gender = users.Gender,
                    ProfilePicture = "data:image/png;base64," + dto.ProfilePicture
                };
                return updatedUserDto;
            }
        }

        public async Task<UsersDto?> GetUserByUserName(string username)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return null;
            }
            else
            { 
                UsersDto userDto = new UsersDto
                {
                    Bio = user.Bio,
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    UsersId = user.UsersId,
                    ProfilePicture = user.ProfilePicture != null ? "data:image/png;base64," + Convert.ToBase64String(user.ProfilePicture) : null,
                    Gender = user.Gender
                };
                return userDto;
            }
        }

        public async Task<bool?> RemoveProfilePictureByUserName(string username)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return false;
            }
            else
            {
                user.ProfilePicture = null; // Remove profile picture
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                UsersDto userDto = new UsersDto
                {
                    Bio = user.Bio,
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    UsersId = user.UsersId,
                    ProfilePicture = user.ProfilePicture != null ? "data:image/png;base64," + Convert.ToBase64String(user.ProfilePicture) : null,
                    Gender = user.Gender
                };
                return true;
            }
        }

        public async Task<bool?> UpdateProfilePictureByUserName(string username, IFormFile filecollection)
        {
            try
            {
                if (filecollection != null && filecollection.Length > 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        await filecollection.CopyToAsync(stream);
                        byte[] imageBytes = stream.ToArray(); // Direct binary data

                        Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
                        if (user != null)
                        {
                          
                            user.ProfilePicture = imageBytes; // Store binary
                            await _context.SaveChangesAsync();
                            
                            return true;
                        }

                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log exception (optional)
                return null;
            }

        }
       
    }
} 
