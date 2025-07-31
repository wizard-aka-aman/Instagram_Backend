using Instagram.Model.Tables;

namespace Instagram.Model.SavedRepo
{
    public interface ISavedRepository
    {
        Task<bool?> AddSaved(string username, int postId);
        Task<bool?> RemoveSaved(string username, int postId);
        Task<bool?> IsSaved(string username, int postId);
        Task<List<Saved>?> GetAllSaved(string username);
        Task<bool?> AddSavedReel(string username, string publicid);
        Task<bool?> RemoveSavedReel(string username, string publicid);
        Task<bool?> IsSavedReel(string username, string publicid);
        Task<List<SavedReel>?> GetAllSavedReel(string username);

    }
}
