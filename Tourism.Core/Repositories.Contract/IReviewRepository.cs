using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Repositories.Contract
{
    public interface IReviewRepository
    {
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(int id, Review review);
        Task DeleteReviewAsync(int id);
        Task<Review> GetReviewByIdAsync(int ReviewId);
        Task<IEnumerable<ReviewsPlaceDTOs>> GetAllReviewByPlaceIdAsync(int PlaceId);
    }
}
