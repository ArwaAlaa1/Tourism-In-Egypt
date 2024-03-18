using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        private readonly TourismContext context;

        public CategoryRepository(TourismContext context):base(context)

        {
            this.context = context;
        }
        public async Task<Category> GetAsync(int id)
        {
            return await context.Categories.Include(p=>p.Places).ThenInclude(p=>p.Photos).Include(p => p.Places).ThenInclude(p => p.City).FirstAsync(p => p.Id == id);

        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {

            var categories = await context.Categories.Include(p=>p.Places).ThenInclude(p=>p.Photos).Include(p=>p.Places).ThenInclude(p=>p.City).ToListAsync();
            return categories;
        }

        //public async Task<List<Place>> GetAllPlacesBySpecificCategory(int id)
        //{
        //    return await context.Places.Where(p => p.CategoryId == id).Include("City").Include("Photos").ToListAsync();

        //}

        
    }
}
