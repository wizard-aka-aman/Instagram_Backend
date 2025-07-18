using Instagram.Model.DTO;
using Instagram.Model.Tables;

namespace Instagram.Model.PostsRepo
{
    public interface IPostRepository
    {
        Task<bool> CreatePostAsync(PostsDto post);
        Task<DisplayPostDto>? GetPostByIdWithUserNameAsync(int postId,string username);
        Task<IEnumerable<Posts>> GetAllPostsByUserNameAsync(string username);
        Task<bool> LikePost(string postUsername, string likedBy , int postId );
        Task<bool> UnLikePost(string postUsername, string likedBy , int postId );
        Task<bool> DeletePostAsync(int postId);
        Task<bool> AddCommentByPostIdWithUserName(CommentDtoWithPostId dto);
    }
}
