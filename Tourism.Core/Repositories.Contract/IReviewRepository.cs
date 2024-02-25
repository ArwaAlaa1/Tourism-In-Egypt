using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
	public interface IReviewRepository :IGenericRepository<Review>
	{
		Task <IEnumerable<Review> > GetAll();
		Task<Review> GetIdIncludeUser(int id);
		void DeleteAll(IEnumerable<Review> reviews);
	}
}
