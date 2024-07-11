using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism.Core
{
    public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
    {
        public IGenericRepository<T> generic { get; set; }

        public IReviewRepository review { get; set; }
        public IPlace_TripRepository placeTrip { get; set; }
        public IChangePassword changePassword { get; set; }
        int Complet();
    }
}
