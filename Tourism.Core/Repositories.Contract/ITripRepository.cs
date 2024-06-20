using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface ITripRepository
    {
        public Task<IEnumerable<Place_Trip>> GetplacesByIdofTrip(int id);
		public Task<Trip> GetTrip(int id);

	}
}
