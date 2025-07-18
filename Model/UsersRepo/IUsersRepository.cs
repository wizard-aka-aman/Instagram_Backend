using Instagram.Model.DTO;
using Instagram.Model.Tables;

namespace Instagram.Model.UsersRepo
{
    public interface IUsersRepository
    {
        Task<UsersDto?> GetUserByUserName(string username);
        Task<UsersDto?> EditUsersByUserName(string username , UsersDto dto);
        Task<bool?> UpdateProfilePictureByUserName(string username, IFormFile filecollection);
        Task<bool?> RemoveProfilePictureByUserName(string username); 
    }
}
