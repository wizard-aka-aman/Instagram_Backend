using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.FollowRepo
{
    public class FollowRepository : IFollowRepository
    {
        private readonly InstagramContext _context;

        public FollowRepository(InstagramContext context)
        {
            _context = context;
        }
        public async Task<bool> FollowUserAsync(string followerUsername, string followingUsername)
        { 
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followerUsername);
            if (user == null)
            {
                return false; // user does not exist
            }
            Users followingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followingUsername);
            if (followingUser == null)
            {
                return false; // following user does not exist
            }
            Followers existingFollow = await _context.Followers
                .FirstOrDefaultAsync(f => f.UserName == followerUsername && f.FollowerUserName == followingUsername);
            if (existingFollow != null)
            {
                return false; // already following
            }
            var newFollow = new Followers
            {
                UserName = followerUsername,
                FollowerUserName = followingUsername,
                FollowedAt = DateTime.Now
            };
            // Increment following count for the follower
            user.FollowingCount++;

            // Increment follower count for the followed user
            followingUser.FollowersCount++;
            _context.Followers.Add(newFollow); 
            await _context.SaveChangesAsync();

            return true;
        }
         

        public async Task<List<DisplayUserFollower>> GetFollowersAsync(string username)
        {
            List<DisplayUserFollower> displayFollowers = new List<DisplayUserFollower>();

            List<Followers> users =await  _context.Followers.Where(e => e.FollowerUserName == username).ToListAsync();

            foreach (var user in users)
            {
                Users findedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (findedUser != null) {
                    DisplayUserFollower duf = new DisplayUserFollower()
                    {
                        FullName = findedUser.FullName,
                        UserName = findedUser.UserName,
                        ProfilePicture = findedUser.ProfilePicture != null ? Convert.ToBase64String(findedUser.ProfilePicture) : null
                    };
                    displayFollowers.Add(duf);
                }
            }
            return displayFollowers;

        }

        public async Task<List<DisplayUserFollower>> GetFollowingAsync(string username)
        {
            List<DisplayUserFollower> displayFollowers = new List<DisplayUserFollower>();

            List<Followers> users = await _context.Followers.Where(e => e.UserName == username).ToListAsync();

            foreach (var user in users)
            {
                Users findedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (findedUser != null)
                {
                    DisplayUserFollower duf = new DisplayUserFollower()
                    {
                        FullName = findedUser.FullName,
                        UserName = findedUser.UserName,
                        ProfilePicture = findedUser.ProfilePicture != null ? Convert.ToBase64String(findedUser.ProfilePicture) : null
                    };
                    displayFollowers.Add(duf);
                }
            }
            return displayFollowers;
        }
         

        public async Task<bool> IsFollowingAsync(string loggedInUsername, string displayUsername)
        {

            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loggedInUsername);
            if (user == null)
            {
                return false; // user does not exist
            }
            Users followingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == displayUsername);
            if (followingUser == null)
            {
                return false; // following user does not exist
            }

            bool isfollwing = await _context.Followers
                .AnyAsync(f => f.UserName == loggedInUsername && f.FollowerUserName == displayUsername);
            return isfollwing;
        }

        public async Task<bool> UnfollowUserAsync(string followerUsername, string followingUsername)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followerUsername);
            if (user == null)
            {
                return false; // user does not exist
            }
            Users followingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followingUsername);
            if (followingUser == null)
            {
                return false; // following user does not exist
            }
            Followers followers = await _context.Followers
                .FirstOrDefaultAsync(f => f.UserName == followerUsername && f.FollowerUserName == followingUsername);
            if (followers == null)
            {
                return false; // not following
            }
            // Decrement following count for the follower
            user.FollowingCount--;
            // Decrement follower count for the followed user
            followingUser.FollowersCount--;
            _context.Followers.Remove(followers);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
