using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IUnitOfWork<T>:IDisposable where T : BaseEntity
    {
        public IGenericRepository<T> generic { get; set; }
        public IReviewRepository review { get; set; }
        public INotificationRepository notification { get; set; }
        public IPlace_TripRepository placeTrip { get; set; }

        int Complet();
    }
}
