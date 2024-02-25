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
    public class Place_TripRepository :GenericRepository<Place_Trip> , IPlace_TripRepository
    {

        private readonly TourismContext _context;

        public Place_TripRepository(TourismContext context):base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Place_Trip>> GetAllWithPlaceAndTrip()
        {
            return await _context.Place_Trips.Include("Places").Include("Trips").ToListAsync();
        }


        public async Task<Place_Trip> GetOneWithPlaceAndTrip(int id)
        {
            return await _context.Place_Trips.Include("Place").Include("Trip").FirstAsync(x => x.Id == id );
        }

       

    }
}
