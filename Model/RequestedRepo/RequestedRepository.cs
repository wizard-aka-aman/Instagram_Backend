using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Model.RequestedRepo
{
    public class RequestedRepository : IRequestedRepository
    {
        private readonly InstagramContext _context;
        public RequestedRepository(InstagramContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRequest(RequestDto dto)
        {
            Users user = _context.Users.Where(e => e.UserName == dto.UserNameOfReqFrom).FirstOrDefault();
            Requested req = new Requested()
            {
                CreatedAt = DateTime.Now,
                IsReqAccepted = false,
                ProfilePictureOfReqFrom = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                UserNameOfReqFrom = dto.UserNameOfReqFrom,
                UserNameOfReqTo = dto.UserNameOfReqTo,
            };
            _context.Requested.Add(req);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRequest(string UserNameOfReqFrom, string UserNameOfReqTo)
        {
            List<Requested> req = _context.Requested.Where(e => e.UserNameOfReqFrom == UserNameOfReqFrom && e.UserNameOfReqTo == UserNameOfReqTo).ToList();
            if(req.Count >= 1)
            {
                _context.Requested.Remove(req[0]);
                List<Notification> noti =_context.Notification.Where(e => e.UserName == UserNameOfReqTo && e.LoggedInUserName == UserNameOfReqFrom && e.NotificationText == "Requested to follow you.").ToList();
                if(noti.Count >= 1)
                {
                    _context.Notification.Remove(noti[0]);
                }
                await _context.SaveChangesAsync();
                return true;
            }
             return false;
        }

        public async Task<List<Requested>> GetAllRequest(string LoogedInUser)
        {
            return await _context.Requested.Where(e => e.UserNameOfReqTo == LoogedInUser).ToListAsync();
        }

        public bool IsRequest(string UserNameOfReqFrom, string UserNameOfReqTo)
        {
            return _context.Requested.Any(e => e.UserNameOfReqFrom == UserNameOfReqFrom && e.UserNameOfReqTo == UserNameOfReqTo);
        }
    }
}
