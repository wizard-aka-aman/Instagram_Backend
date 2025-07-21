using Instagram.Model.Tables;

namespace Instagram.Model.SavedRepo
{
    public interface ISavedRepository
    {
        Task<bool?> AddSaved(string username, int postId);
        Task<bool?> RemoveSaved(string username, int postId);
        Task<bool?> IsSaved(string username, int postId);
        Task<List<Saved>?> GetAllSaved(string username);

    }
}
