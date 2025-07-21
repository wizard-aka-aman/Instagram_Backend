
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.SavedRepo
{
    public class SavedRepository : ISavedRepository
    {
        private readonly InstagramContext _context;

        public SavedRepository(InstagramContext context)
        {
            _context = context;
        }
        public async Task<bool?> AddSaved(string username, int postId)
        {
            if (string.IsNullOrEmpty(username) || postId <= 0)
            {
                return false;
            }
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if (user == null)
            {
                return false;
            }
            Posts savedPosts = await _context.Posts.FirstOrDefaultAsync(e => e.PostId == postId);
            if (savedPosts == null)
            {
                return false;
            }
            Saved isAlreadyPresent =await _context.Saved.Include(e => e.Posts).FirstOrDefaultAsync(e => e.UserName == username && e.Posts.PostId == postId);
            if (isAlreadyPresent != null) { 
                return false;
            }
            Saved saved = new Saved
            {
                Posts = savedPosts,
                SavedAt = DateTime.Now,
                UserName = username,
            };
            _context.Saved.Add(saved);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Saved>?> GetAllSaved(string username)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if(user == null)
            {
                return null;    
            }
           return await _context.Saved.Include(e => e.Posts).ThenInclude(e => e.User)
                   .Where(p => p.UserName == username).OrderByDescending(e => e.SavedAt)
                   .ToListAsync();
        }

        public async  Task<bool?> IsSaved(string username, int postId)
        {
            //if (string.IsNullOrEmpty(username) || postId <= 0)
            //{
            //    return null;
            //}
            //Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            //if (user == null)
            //{
            //    return null;
            //}
            //Posts savedPosts = await _context.Posts.FirstOrDefaultAsync(e => e.PostId == postId);
            //if (savedPosts == null)
            //{
            //    return null;
            //}

            Saved isAlreadyPresent = await _context.Saved.FirstOrDefaultAsync(e => e.UserName == username && e.Posts.PostId == postId);
            if (isAlreadyPresent == null)
            {
                return false;
            }
            return true;  
        }

        public async Task<bool?> RemoveSaved(string username, int postId)
        {
            if (string.IsNullOrEmpty(username) || postId <= 0)
            {
                return false;
            }
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if (user == null)
            {
                return false;
            }
            Posts savedPosts = await _context.Posts.FirstOrDefaultAsync(e => e.PostId == postId);
            if (savedPosts == null)
            {
                return false;
            }

            Saved isAlreadyPresent = await _context.Saved.Include(e => e.Posts).FirstOrDefaultAsync(e => e.UserName == username && e.Posts.PostId == postId);
            if (isAlreadyPresent == null)
            {
                return false;
            }
            _context.Saved.Remove(isAlreadyPresent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
