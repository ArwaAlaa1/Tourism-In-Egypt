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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly TourismContext context;

        public CityRepository(TourismContext context):base(context)
        {
            this.context = context;
        }
        public async Task<List<CityPhotos>> GetAllPhotoBySpecIdAsync(int id)
        {
            return await context.CityPhotos.Where(c => c.CityId == id).ToListAsync();
        }
    }
}
