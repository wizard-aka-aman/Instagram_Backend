using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.ReelRepo
{
    public class ReelRepository : IReelRepository
    {
        private readonly InstagramContext _context;

        public ReelRepository(InstagramContext context)
        {
            _context = context;
        }
        public async Task<bool?> CommentReel(CommentDtoWithPublicid dto)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == dto.UserName);
            if (user == null)
            {
                return false; // User does not exist
            }

            CloudinaryDB reel = await _context.CloudinaryDB.FirstOrDefaultAsync(e => e.PublicId == dto.publicid);
            if (reel== null)
            {
                return false; // Post does not exist
            }

            reel.CommentCount++;
            ReelComment comment = new ReelComment
            {
                publicId = dto.publicid,
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                UserName = user.UserName,
                CommentedAt = DateTime.Now,
                CommentText = dto.CommentText
            };
            _context.ReelComment.Add(comment);
            _context.CloudinaryDB.Update(reel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DisplayReelDto>? GetReelByPublicid(string publicid)
        {
            if (publicid == "")
            {
                return null;
            }
            // Fetch the post by ID 
            CloudinaryDB reel = await _context.CloudinaryDB
               .FirstOrDefaultAsync(p => p.PublicId == publicid );
            if (reel == null)
            {
                return null;
            }
            // Check if the user exists
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == reel.UserName);
            if (user == null)
            {
                return null; // User does not exist
            }

            List<ReelComment> comment = await _context.ReelComment.Where(c => c.publicId == publicid).ToListAsync();
            List<ReelLike> likes = await _context.ReelLike.Where(c => c.publicId == publicid).ToListAsync();

            DisplayReelDto displayReel = new DisplayReelDto
            {
                Descripton = reel.Description,
                CreatedAt = reel.CreatedAt,
                Url = reel.Url,
                Publicid = publicid,
                ReelId = reel.Id,
                Comments = comment.Select(c => new ReelCommentDto
                {
                    CommentedAt = c.CommentedAt,
                    CommentText = c.CommentText,
                    UserName = c.UserName,
                    ProfilePicture = c.ProfilePicture
                }).OrderByDescending(e => e.CommentedAt).ToList(),
                LikesCount = reel.LikeCount,
                CommentsCount = reel.CommentCount,
                Likes = likes.Select(l => new ReelLikeDto
                {
                    LikedAt = l.LikedAt,
                    UserName = l.UserName,
                    ProfilePicture = l.ProfilePicture
                }).OrderByDescending(e => e.LikedAt).ToList(),
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                UserName = user.UserName
            };
            return displayReel;
        }

        public async Task<bool?> LikeReel(string likedBy, string publicid)
        {

            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == likedBy);
            if (user == null)
            {
                return false; // User does not exist
            }

            CloudinaryDB reel = await _context.CloudinaryDB.FirstOrDefaultAsync(e => e.PublicId == publicid);
            if (reel == null)
            {
                return false; // Reel does not exist
            }

            // Check if the user has already liked the post
            ReelLike existingLike = await _context.ReelLike
                .FirstOrDefaultAsync(l => l.publicId == publicid && l.UserName == likedBy);
            if (existingLike == null)
            {
                reel.LikeCount++;
                ReelLike like = new ReelLike
                {
                    publicId = publicid,
                    ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                    UserName = user.UserName,
                    LikedAt = DateTime.Now
                };
                _context.ReelLike.Add(like);
                _context.CloudinaryDB.Update(reel);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool?> UnLikeReel(string likedBy, string publicid)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == likedBy);
            if (user == null)
            {
                return false; // User does not exist
            }

            CloudinaryDB reel = await _context.CloudinaryDB.FirstOrDefaultAsync(e=> e.PublicId == publicid);
            if (reel == null)
            {
                return false; // Reel does not exist
            }

            // Check if the user has already liked the post
            ReelLike existingLike = await _context.ReelLike
                .FirstOrDefaultAsync(l => l.publicId == publicid && l.UserName== likedBy);
            if (existingLike != null)
            {
                if (reel.LikeCount> 0)
                    reel.LikeCount--;
                _context.ReelLike.Remove(existingLike);
                _context.CloudinaryDB.Update(reel);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<DisplayReelDto>> GetFiveReel(string loggedInUser)
        {

            // Fetch the post by ID 
            List<CloudinaryDB> reel = await _context.CloudinaryDB
               .ToListAsync();

            if (reel == null || reel.Count == 0)
                return new List<DisplayReelDto>();
            // Check if the user exists
            List<DisplayReelDto> displayReels = new List<DisplayReelDto>();
            foreach (var r in reel)
            {
                Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == r.UserName);
                if (user == null)
                {
                    continue; // User does not exist
                }
                List<ReelComment> comment = await _context.ReelComment.Where(c => c.publicId == r.PublicId).ToListAsync();
                List<ReelLike> likes = await _context.ReelLike.Where(c => c.publicId == r.PublicId).ToListAsync();
                DisplayReelDto displayReel = new DisplayReelDto
                {
                    Descripton = r.Description,
                    CreatedAt = r.CreatedAt,
                    Url = r.Url,
                    Publicid = r.PublicId,
                    ReelId = r.Id,
                     IsLikedLoggedInUser = likes.Any(l => l.UserName == loggedInUser),
                    Comments = comment.Select(c => new ReelCommentDto
                    {
                        CommentedAt = c.CommentedAt,
                        CommentText = c.CommentText,
                        UserName = c.UserName,
                        ProfilePicture = c.ProfilePicture
                    }).ToList(),
                    LikesCount = r.LikeCount,
                    CommentsCount = r.CommentCount,
                    Likes = likes.Select(l => new ReelLikeDto
                    {
                        LikedAt = l.LikedAt,
                        UserName = l.UserName,
                        ProfilePicture = l.ProfilePicture
                    }).OrderByDescending(e => e.LikedAt).ToList(),
                    ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                    UserName = user.UserName
                };
                displayReels.Add(displayReel);
            }

            return displayReels;  

            //List<ReelComment> comment = await _context.ReelComment.Where(c => c.publicId == publicid).ToListAsync();
            //List<ReelLike> likes = await _context.ReelLike.Where(c => c.publicId == publicid).ToListAsync();

            //DisplayReelDto displayReel = new DisplayReelDto
            //{
            //    Descripton = reel.Description,
            //    CreatedAt = reel.CreatedAt,
            //    Url = reel.Url,
            //    Publicid = publicid,
            //    ReelId = reel.Id,
            //    Comments = comment.Select(c => new ReelCommentDto
            //    {
            //        CommentedAt = c.CommentedAt,
            //        CommentText = c.CommentText,
            //        UserName = c.UserName,
            //        ProfilePicture = c.ProfilePicture
            //    }).OrderByDescending(e => e.CommentedAt).ToList(),
            //    LikesCount = reel.LikeCount,
            //    CommentsCount = reel.CommentCount,
            //    Likes = likes.Select(l => new ReelLikeDto
            //    {
            //        LikedAt = l.LikedAt,
            //        UserName = l.UserName,
            //        ProfilePicture = l.ProfilePicture
            //    }).OrderByDescending(e => e.LikedAt).ToList(),
            //    ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
            //    UserName = user.UserName
            //}; 
        }
    }
}
