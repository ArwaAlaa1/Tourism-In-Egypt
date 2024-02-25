using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;


namespace Tourism.Repository.Repository
{
	public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly TourismContext _context;

        public NotificationRepository(TourismContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAll()
        {
            return _context.Notifications.Include("User").ToList();
        }

		public async Task<Notification> GetNotificationwithUser(int id)
		{
			return _context.Notifications.Include("User").FirstOrDefault(x => x.Id == id);
		}



        //public NotificationModel CreatebyNoModel()
        //{
        //    var userlist =  _context.generic.GetAllAsync();
        //    NotificationModel model = new NotificationModel()
        //    {
        //        users = userlist
        //    };
        //}
    }
}
