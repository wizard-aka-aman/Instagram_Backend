
using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Instagram.Model.NotificationRepo
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly InstagramContext _context; 
        public NotificationRepository(InstagramContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNotification(AddNotificationDto dto)
        {
            Users user =  _context.Users.Where(e => e.UserName == dto.LoggedInUser).FirstOrDefault();
            List<Posts> p = new List<Posts>();
            if (dto.PostId != null)
            {
                p = _context.Posts.Include(e => e.User).Where(e => e.PostId == dto.PostId).ToList();
                dto.UserName = p.Count() >= 1 ? p[0].User.UserName : "";
            }
            List<CloudinaryDB> c = new List<CloudinaryDB>();
            if (dto.reelId!= null)
            {
                c = _context.CloudinaryDB.Where(e => e.PublicId== dto.reelId).ToList();
                dto.UserName = c.Count() >= 1 ? c[0].UserName : "";
            }
            Notification notification = new Notification()
            {
                UserName = dto.UserName,
                NotificationText = dto.Message,
                CreatedAt = DateTime.Now,
                IsSeen = false,
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                LoggedInUserName = dto.LoggedInUser,
                SeenAt = null,
                PostId = p.Count()>=1 ? p[0] : null,
                reelId = c.Count()>=1 ? c[0] : null,
            };
            if (notification.UserName == notification.LoggedInUserName) {
                return false;
            }
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public List<NotificationDto> GetAllNotification(string LoogedInUser)
        {
            var result = _context.Notification.Where(e => e.UserName == LoogedInUser).Include(e => e.PostId).Select(e => new NotificationDto
            {
                UserName = e.UserName,
                NotificationText = e.NotificationText,
                Id = e.Id,
                IsSeen = e.IsSeen,
                ProfilePicture = e.ProfilePicture,
                CreatedAt= e.CreatedAt,
                LoggedInUserName = e.LoggedInUserName,
                PostId= e.PostId,
                reelId= e.reelId,
            }).OrderByDescending(e => e.CreatedAt).ToList();
            return result;
        }

        public async Task<bool> SeenNotification(string LoggedInUser)
        {
           List<Notification> notification =   _context.Notification.Where(e => e.UserName == LoggedInUser).ToList();

            foreach (var item in notification)
            {
                if (item.IsSeen == false) { 
                    item.IsSeen = true;
                    item.SeenAt = DateTime.Now;
                }
            }
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
