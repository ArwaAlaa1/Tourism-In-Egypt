
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Repositories.Contract
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
        Task<IEnumerable<NotificationReviewDTO>> GetAllNotificationAsync();

    }
}
