using CorePush.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;
using static Tourism.Core.Entities.GoogleNotification;

namespace Tourism.Repository.Repository
{

    public class NotificationRepository : INotificationRepository
    {
        private readonly TourismContext _context;



        public NotificationRepository(IOptions<FcmNotificationSetting> settings , TourismContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllNotificationAsync()
        {
            var notifications = await _context.Reviews.Select(r => new NotificationDTO
            {
                PlaceId = r.PlaceId,
                Time = r.Time,
               // UserImg = r.User.ImgURL,
                UserName = r.User.UserName
            }).ToListAsync();


            return notifications;
        }

    }
}