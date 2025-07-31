
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
        public async Task<bool?> AddSavedReel(string username, string publicid)
        {
            if (string.IsNullOrEmpty(username) || publicid == "")
            {
                return false;
            }
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if (user == null)
            {
                return false;
            }
            CloudinaryDB savedReel= await _context.CloudinaryDB.FirstOrDefaultAsync(e => e.PublicId == publicid);
            if (savedReel == null)
            {
                return false;
            }
            SavedReel isAlreadyPresent =await _context.SavedReel.Include(e => e.CloudinaryDB).FirstOrDefaultAsync(e => e.UserName == username && e.CloudinaryDB.PublicId== publicid);
            if (isAlreadyPresent != null) { 
                return false;
            }
            SavedReel saved = new SavedReel
            {
                CloudinaryDB = savedReel,
                SavedAt = DateTime.Now,
                UserName = username,
            };
            _context.SavedReel.Add(saved);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SavedReel>?> GetAllSavedReel(string username)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if(user == null)
            {
                return null;    
            }
           return await _context.SavedReel.Include(e => e.CloudinaryDB)
                   .Where(p => p.UserName == username).OrderByDescending(e => e.SavedAt)
                   .ToListAsync();
        }

        public async  Task<bool?> IsSavedReel(string username, string publicid)
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

            SavedReel isAlreadyPresent = await _context.SavedReel.FirstOrDefaultAsync(e => e.UserName == username && e.CloudinaryDB.PublicId== publicid);
            if (isAlreadyPresent == null)
            {
                return false;
            }
            return true;  
        }

        public async Task<bool?> RemoveSavedReel(string username, string publicid)
        {
            if (string.IsNullOrEmpty(username) || publicid== "")
            {
                return false;
            }
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if (user == null)
            {
                return false;
            }
            SavedReel savedReel = await _context.SavedReel.Include(e=> e.CloudinaryDB).FirstOrDefaultAsync(e => e.CloudinaryDB.PublicId == publicid);
            if (savedReel == null)
            {
                return false;
            }

            SavedReel isAlreadyPresent = await _context.SavedReel.Include(e => e.CloudinaryDB).FirstOrDefaultAsync(e => e.UserName == username && e.CloudinaryDB.PublicId == publicid);
            if (isAlreadyPresent == null)
            {
                return false;
            }
            _context.SavedReel.Remove(isAlreadyPresent);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
