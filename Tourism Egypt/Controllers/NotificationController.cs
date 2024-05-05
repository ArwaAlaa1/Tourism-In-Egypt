using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Repository;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotification()
        {

            var notificationReviews = await _notificationService.GetAllNotificationAsync();

            if (notificationReviews == null)
            {
                return NotFound("No Existing Notification");
            }
            else
                return Ok(notificationReviews);
        }

    }
}