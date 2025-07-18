using Instagram.Model.DTO;

namespace Instagram.Model.FollowRepo
{
    public interface IFollowRepository
    {
        Task<bool> FollowUserAsync(string followerUsername, string followingUsername);
        Task<bool> UnfollowUserAsync(string followerUsername, string followingUsername);
        Task<bool> IsFollowingAsync(string followerUsername, string followingUsername);
        Task<List<DisplayUserFollower>> GetFollowersAsync(string username);
        Task<List<DisplayUserFollower>> GetFollowingAsync(string username);
        //Task<int> GetFollowerCountAsync(string username);
        //Task<int> GetFollowingCountAsync(string username);
    }
}
