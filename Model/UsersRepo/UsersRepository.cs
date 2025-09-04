using Instagram.Model.DTO;
using Instagram.Model.Tables; 
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

        public async Task<UsersDto?> EditUsersByUserName(string username, EditUserDto dto)
        {
            Users users = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (users == null)
            {
                return null;
            }
            else
            {
                users.Bio = dto.Bio; 
                users.FullName = dto.FullName; 
                users.Gender = dto.Gender;
                users.IsPublic = dto.IsPublic;
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
                    IsPublic = users.IsPublic,
                };
                return updatedUserDto;
            }
        }

        public async Task<List<DisplayUserFollower>> GetAllUsers()
        {
            List<DisplayUserFollower> list = new List<DisplayUserFollower>();
            var all = _context.Users.ToList();
            foreach (var item in all)
            {
                DisplayUserFollower duf = new DisplayUserFollower
                {
                    FullName = item.FullName,
                    ProfilePicture = item.ProfilePicture != null ? Convert.ToBase64String(item.ProfilePicture) : null,
                    UserName = item.UserName,
                };
                list.Add(duf);
            }
            return list;

        }

        public async Task<UsersDto?> GetUserByUserName(string username)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            var followcount = _context.Followers.Count(e => e.FollowerUserName == username);
            var followingcount = _context.Followers.Count(e => e.UserName == username);
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
                    ProfilePicture = user.ProfilePicture != null ?  Convert.ToBase64String(user.ProfilePicture) : null,
                    Gender = user.Gender,
                    FollowersCount = followcount,
                    FollowingCount = followingcount,
                    IsPublic = user.IsPublic
                    
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
                    ProfilePicture = user.ProfilePicture != null ?Convert.ToBase64String(user.ProfilePicture) : null,
                    Gender = user.Gender,
                    IsPublic= user.IsPublic
                };
                return true;
            }
        }

        public async Task<List<DisplayUserFollower>> Search(string query)
        {
            query = query.ToLower();
            var allusers = await GetAllUsers();
            List<DisplayUserFollower> list = new List<DisplayUserFollower>();
            foreach (var user in allusers) {
                var userName = user.UserName.ToLower(); 
                if (userName.Contains(query))
                list.Add(user);
            }
            return list;
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
