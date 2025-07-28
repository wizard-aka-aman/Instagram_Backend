
using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.StoryRepo
{
    public class StoryRepository : IStoryRepository
    {
        private readonly InstagramContext _context;

        public StoryRepository(InstagramContext context)
        {
            _context = context;
        }
        public async Task<bool?> AddStory(StoryDto dto)
        {
            var img = await Image(dto.Username, dto.imageFile);
            Story s = new Story
            {
                CreatedAt = DateTime.Now,
                Username = dto.Username,
                ExpirationTime = DateTime.Now.AddHours(24),
                ImageUrl = img,
            };
            _context.Story.Add(s);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool?> DeleteStory(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DisplayStoryWithGroup>> GetStoriesByUser(string username)
        {

            List<DisplayStoryWithGroup> dswg = new List<DisplayStoryWithGroup>();
            List<StorySeen> seen = await _context.StorySeen
                .Where(s => s.SeenByUsername == username)
                .ToListAsync();

            List<Followers> users = await _context.Followers.Where(e => e.UserName == username).ToListAsync();

            foreach (var user in users)
            {
                Users findedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.FollowerUserName);
                if (findedUser != null)
                {
            List<DisplayStoryDto> dsd = new List<DisplayStoryDto>();
                    var totalstory = await _context.Story.Where(e => e.Username == findedUser.UserName && e.ExpirationTime > DateTime.Now).ToListAsync();
                    if(totalstory.Count != 0)
                    {                    
                    foreach (var item in totalstory)
                    { 
                            DisplayStoryDto displayStoryDto = new DisplayStoryDto
                            {
                                StoryId = item.StoryId,
                                ImageUrl = item.ImageUrl,
                                CreatedAt = HoursSincePost(item.CreatedAt.ToString()) , 
                                ExpirationTime = item.ExpirationTime
                            };
                        dsd.Add(displayStoryDto);

                    }
                    DisplayStoryWithGroup displayStory = new DisplayStoryWithGroup
                    {

                        FullName = findedUser.FullName,
                        Username = findedUser.UserName,
                        ProfilePicture = findedUser.ProfilePicture != null ? Convert.ToBase64String(findedUser.ProfilePicture) : null,
                        DisplayStories = dsd
                    };
                    dswg.Add(displayStory);
                    }
                }


            } 
            // Mark stories as seen if the user has viewed them
            foreach (var seenStory in seen)
            {
                foreach (var userGroup in dswg)
                {
                    foreach (var story in userGroup.DisplayStories)
                    {
                        if (story.StoryId == seenStory.StoryId)
                        {
                            story.IsSeen = true;
                        }
                    }
                }
            } 
            dswg.ForEach(s =>
            {
                s.IsSeen = s.DisplayStories.All(ds => ds.IsSeen);
            });
            // Now sort by IsSeen = false first
            dswg = dswg.OrderBy(s => s.IsSeen).ToList();

            return dswg;
        }
        public async Task<List<DisplayStoryWithGroup>> GetPersonalStories(string username)
        {

            List<DisplayStoryWithGroup> dswg = new List<DisplayStoryWithGroup>();
            List<StorySeen> seen = await _context.StorySeen
                .Where(s => s.SeenByUsername == username)
                .ToListAsync();

            Users users = await _context.Users.FirstOrDefaultAsync(e => e.UserName == username);
 
                
                if (users != null)
                {
                    List<DisplayStoryDto> dsd = new List<DisplayStoryDto>();
                    var totalstory = await _context.Story.Where(e => e.Username == users.UserName && e.ExpirationTime > DateTime.Now).ToListAsync();
                    if (totalstory.Count != 0)
                    {
                        foreach (var item in totalstory)
                        {
                        var whoViwed = await _context.StorySeen.Where(e => e.StoryId == item.StoryId).ToListAsync();

                        DisplayStoryDto displayStoryDto = new DisplayStoryDto
                            {
                                StoryId = item.StoryId,
                                ImageUrl = item.ImageUrl,
                                CreatedAt = HoursSincePost(item.CreatedAt.ToString()),
                                ExpirationTime = item.ExpirationTime,
                                SeenBy = whoViwed,
                        };
                            dsd.Add(displayStoryDto);

                        }
                        DisplayStoryWithGroup displayStory = new DisplayStoryWithGroup
                        {

                            FullName = users.FullName,
                            Username = users.UserName,
                            ProfilePicture = users.ProfilePicture != null ? Convert.ToBase64String(users.ProfilePicture) : null,
                            DisplayStories = dsd,
                            
                        };
                        dswg.Add(displayStory);
                    }
                } 
            // Mark stories as seen if the user has viewed them
            foreach (var seenStory in seen)
            {
                foreach (var userGroup in dswg)
                {
                    foreach (var story in userGroup.DisplayStories)
                    {
                        if (story.StoryId == seenStory.StoryId)
                        {
                            story.IsSeen = true;
                        }
                    }
                }
            }
            dswg.ForEach(s =>
            {
                s.IsSeen = s.DisplayStories.All(ds => ds.IsSeen);
            });
            // Now sort by IsSeen = false first
            dswg = dswg.OrderBy(s => s.IsSeen).ToList();

            return dswg;
        }
        public async Task<bool> MarkStoryAsSeen(int storyId, string seenBy)
        {
            var alreadySeen = await _context.StorySeen
                .FirstOrDefaultAsync(s => s.StoryId == storyId && s.SeenByUsername == seenBy);
            Users user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == seenBy);
            if (user == null) {
                return false;
            }
            if (alreadySeen == null)
            {
                var seen = new StorySeen
                {
                    StoryId = storyId,
                    SeenByUsername = seenBy,
                    SeenAt = DateTime.Now,
                    ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null

                };

                _context.StorySeen.Add(seen);
                await _context.SaveChangesAsync();
            }

            return true;
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
                    return base64Image;  
                }  
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<StorySeen>> GetStoryViewers(string username)
        {
            return await _context.StorySeen
                .Where(s => s.SeenByUsername == username)
                .ToListAsync();
        }
        public double HoursSincePost(string postDateTimeString)
        {
            DateTime postTime = DateTime.Parse(postDateTimeString);
            DateTime currentTime = DateTime.Now;

            TimeSpan diff = currentTime - postTime;
            double diffInHours = diff.TotalHours;

            return Convert.ToInt32(diffInHours);
        }

        public async Task<DisplayStoryWithGroup?> IsStoryAvailable(string username)
        {
            var stories = await _context.Story
                .Where(s => s.Username == username && s.ExpirationTime > DateTime.Now)
                .ToListAsync();
 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                return new DisplayStoryWithGroup { };
            }
            List<DisplayStoryDto> displayStories = new List<DisplayStoryDto>();
            foreach (var story in stories)
            {
                displayStories.Add(new DisplayStoryDto
                {
                    StoryId = story.StoryId,
                    ImageUrl = story.ImageUrl,
                    CreatedAt = HoursSincePost(story.CreatedAt.ToString()),
                    ExpirationTime = story.ExpirationTime
                });
            }
            return new DisplayStoryWithGroup
            {
                FullName = user.FullName,
                Username = user.UserName,
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                DisplayStories = displayStories
            };
        }
    }
}
