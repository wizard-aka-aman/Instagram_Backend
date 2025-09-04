using Instagram.Model.DTO;
using Instagram.Model.Tables;

namespace Instagram.Model.StoryRepo
{
    public interface IStoryRepository
    {
        Task<bool?> AddStory(StoryDto dto);
        Task<bool?> DeleteStory(string username); 
        Task<List<DisplayStoryWithGroup>> GetStoriesByUser(string username);
        Task<List<DisplayStoryWithGroup>> GetPersonalStories(string username);
        Task<bool> MarkStoryAsSeen(int storyId, string seenBy);
        Task<List<StorySeen>> GetStoryViewers(string username);
        Task<DisplayStoryWithGroup?> IsStoryAvailable(string username);
        Task<List<DisplayPostDto>> DisplayPostHome(string username);
    }
}
