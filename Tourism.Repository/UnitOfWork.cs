using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;
using Tourism.Repository.Repository;

namespace Tourism.Repository
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntity
    {
        public IGenericRepository<T> generic { get ; set; }
        public IReviewRepository review { get; set; }
        public IPlace_TripRepository placeTrip { get; set; }
        public INotificationRepository notification { get; set; }
        private readonly TourismContext _context;


        public UnitOfWork(TourismContext context)
        {
           _context = context;
            generic = new GenericRepository<T>(_context);
            review = new ReviewRepository(_context);
            notification = new NotificationRepository(_context);
            placeTrip = new Place_TripRepository(_context);
        }

       
        public int Complet()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
