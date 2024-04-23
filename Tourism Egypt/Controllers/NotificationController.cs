using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

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
    }
}