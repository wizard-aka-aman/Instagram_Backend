using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.PostsRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly InstagramContext _context;

        public PostRepository(InstagramContext context)
        {
            _context = context;
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
                .Where(p => p.User.UserName == username)
                .ToListAsync();

        }

        public Task<Posts?> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }
        public  async Task<string?> Image(string username, IFormFile filecollection)
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
    }

}
