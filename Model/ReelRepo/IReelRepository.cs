using Instagram.Model.DTO;

namespace Instagram.Model.ReelRepo
{
    public interface IReelRepository
    {
        Task<bool?> LikeReel( string likedBy, string publicid);
        Task<bool?> UnLikeReel(string likedBy, string publicid);
        Task<bool?> CommentReel(CommentDtoWithPublicid dto);
        Task<DisplayReelDto>? GetReelByPublicid(string publicid);
        Task<List<DisplayReelDto>> GetFiveReel(string loggedInUser);
    }
}
