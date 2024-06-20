﻿using Microsoft.EntityFrameworkCore;
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
    public class TripRepository:GenericRepository<Trip> , ITripRepository
    {
        private readonly TourismContext context;

        public TripRepository(TourismContext context):base(context)
        {
            this.context = context;
        }

		public async Task<Trip> GetTrip(int id)
		{
			return await context.Trips.Where(x => x.Id == id).Include(p=>p.Places).ThenInclude(p=>p.Photos).FirstAsync(p => p.Id == id);

		}

		public async Task<IEnumerable<Place_Trip>> GetplacesByIdofTrip(int id)
        {
            return await  context.Place_Trips.Where(x => x.TripId == id).Include("Place").ToListAsync();

        }
    }
}
