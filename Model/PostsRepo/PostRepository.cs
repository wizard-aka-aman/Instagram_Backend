using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Instagram.Model.PostsRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly InstagramContext _context;

        public PostRepository(InstagramContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCommentByPostIdWithUserName(CommentDtoWithPostId dto)
        {
           
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == dto.UserName);
            if (user == null)
            {
                return false; // User does not exist
            }

            Posts post = await _context.Posts.FirstOrDefaultAsync(e => e.PostId == dto.PostId);
            if (post == null)
            {
                return false; // Post does not exist
            }
             
                post.CommentsCount++;
                Comments comment = new Comments
                {
                    PostId = post.PostId,
                    ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                    UserName = user.UserName,
                    CommentedAt = DateTime.Now,
                    CommentText = dto.CommentText
                };
                _context.Comments.Add(comment);
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
          
        } 

        public async Task<bool> CreatePostAsync(PostsDto dto)
        {
            if (dto == null || dto.UserName == "")
            {
                return false;
            }
            // Check if the user exists
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == dto.UserName);
            if (user == null)
            {
                return false; // User does not exist
            }
            var img = await Image(user.UserName, dto.imageFile);// Assuming Image method is static and handles image upload
            Posts post = new Posts
            {
                Caption = dto.Caption,
                ImageUrl = img,
                UserId = user.UsersId,
                User = user, // Set the user reference
                CreatedAt = DateTime.UtcNow
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return true;

        }

        public Task<bool> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Posts>> GetAllPostsByUserNameAsync(string username)
        {
            // Fetch all posts by the username
            return await _context.Posts.Include(e => e.User)
                .Where(p => p.User.UserName == username).OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

        }

        public async Task<DisplayPostDto>? GetPostByIdWithUserNameAsync(int postId, string username)
        {
            if (postId <= 0)
            {
                return null;
            }
            // Check if the user exists
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
            if (user == null)
            {
                return null; // User does not exist
            }
            // Fetch the post by ID 
            Posts posts = await _context.Posts
               .FirstOrDefaultAsync(p => p.PostId == postId && user.UserName == username);
            if (posts == null)
            {
                return null;
            }
            List<Comments> comment = await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
            List<Likes> likes = await _context.Likes.Where(c => c.PostId == postId).ToListAsync();

            DisplayPostDto displayPost = new DisplayPostDto
            {
                Caption = posts.Caption,
                CreatedAt = posts.CreatedAt,
                ImageUrl =    posts.ImageUrl,
                PostId = posts.PostId,
                Comments = comment.Select(c => new CommentDto
                {
                    CommentedAt = c.CommentedAt,
                    CommentText = c.CommentText,
                    UserName = c.UserName,
                    ProfilePicture =   c.ProfilePicture
                }).OrderByDescending(e => e.CommentedAt).ToList(),
                LikesCount = posts.LikesCount,
                CommentsCount = posts.CommentsCount,
                Likes = likes.Select(l => new LikeDto
                {
                    LikedAt = l.LikedAt,
                    UserName = l.UserName,
                    ProfilePicture =l.ProfilePicture
                }).OrderByDescending(e => e.LikedAt).ToList(),
                ProfilePicture =    Convert.ToBase64String(user.ProfilePicture),
                UserName = user.UserName
            };
            return displayPost;
        }
        public async Task<string?> Image(string username, IFormFile filecollection)
        {
            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    await filecollection.CopyToAsync(stream);

                    // Convert to base64 string
                    byte[] imageBytes = stream.ToArray();
                    string base64Image = Convert.ToBase64String(imageBytes);

                    //expensedto.Image = base64Image;

                    // OPTIONAL: You should map DTO to your Expense entity and update DB here
                    return base64Image; // Return the base64 string

                }



            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> LikePost(string postUsername, string likedBy, int postId)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == likedBy);
            if (user == null)
            {
                return false; // User does not exist
            }

            Posts post = await _context.Posts.FirstOrDefaultAsync(e => e.PostId == postId);
            if (post == null)
            {
                return false; // Post does not exist
            }

            // Check if the user has already liked the post
            Likes existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserName == user.UserName);
            if (existingLike == null)
            {
                post.LikesCount++;
                Likes like = new Likes
                {
                    PostId = post.PostId,
                    ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture):null,
                    UserName = user.UserName,
                    LikedAt = DateTime.UtcNow
                };
                _context.Likes.Add(like);
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UnLikePost(string postUsername, string likedBy, int postId)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(e => e.UserName == likedBy);
            if (user == null)
            {
                return false; // User does not exist
            }

            Posts post = await _context.Posts.FirstOrDefaultAsync(e => e.PostId == postId);
            if (post == null)
            {
                return false; // Post does not exist
            }

            // Check if the user has already liked the post
            Likes existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserName == user.UserName);
            if (existingLike != null)
            {
                if(post.LikesCount >0)
                post.LikesCount--;
                _context.Likes.Remove(existingLike);
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

}
