using Instagram.Model.DTO;
using Instagram.Model.Tables;

namespace Instagram.Model.PostsRepo
{
    public interface IPostRepository
    {
        Task<bool> CreatePostAsync(PostsDto post);
        Task<Posts?> GetPostByIdAsync(int postId);
        Task<IEnumerable<Posts>> GetAllPostsByUserNameAsync(string username);
        //Task<bool> UpdatePostAsync(Posts post);
        Task<bool> DeletePostAsync(int postId); 
    }
}
