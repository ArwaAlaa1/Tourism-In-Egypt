
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
