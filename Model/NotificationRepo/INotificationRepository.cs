using Instagram.Model.DTO;

namespace Instagram.Model.NotificationRepo
{
    public interface INotificationRepository
    {
        Task<bool> AddNotification(AddNotificationDto dto);
        Task<bool> SeenNotification(string LoggedInUser);
        List<NotificationDto> GetAllNotification(string LoogedInUser); 
    }
}
