using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IPlace_TripRepository : IGenericRepository<Place_Trip>
    {
        Task<Place_Trip >GetOneWithPlaceAndTrip(int id);
        Task<IEnumerable<Place_Trip>> GetAllWithPlaceAndTrip();

        Task<Place_Trip> ForUnique(int valueofplace, int trip);


    }
}
