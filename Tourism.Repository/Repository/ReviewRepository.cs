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
	public class ReviewRepository : GenericRepository<Review>,IReviewRepository
	{
		private readonly TourismContext _context; 
        public ReviewRepository(TourismContext context):base(context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Review>> GetAll()
		{
			return  _context.Reviews.Include("User").Include("Place").ToList();
		}


		public void DeleteAll(IEnumerable<Review> reviews)
		{
			
			_context.Remove(reviews);
		}

		public async Task<Review> GetIdIncludeUser(int id)
		{
			return _context.Reviews.Include("User").FirstOrDefault(x => x.Id == id);
		}
	}

}
