
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class ReviewController : BaseApiController
    {
        public readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewDTO review)
        {
            try
            {
                var mappedReview = _mapper.Map<AddReviewDTO, Review>(review);

                var addedReview = await _reviewRepository.AddReviewAsync(mappedReview);

               return Ok(new Response()
                {
                    Status = true,
                    Message = $"Added Successfully"
                }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Status = false,
                    Message = $"An error occurred while add Review: {ex.Message}"
                });
            }
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewDTO review)
        {
            try
            {

                var mappedReview = _mapper.Map<UpdateReviewDTO, Review>(review);

                var updatedReview = await _reviewRepository.UpdateReviewAsync(id, mappedReview);
                return Ok(new Response()
                {
                    Status = true,
                    Message = $"Updated Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Status = false,
                    Message = $"An error occurred while Updated Review: {ex.Message}"
                });
            }
        }

        [HttpDelete("[action]/{ReviewId}")]
        public async Task<IActionResult> DeleteReview(int ReviewId)
        {
            try
            {
                var existingReview = await _reviewRepository.GetReviewByIdAsync(ReviewId);

                if (existingReview == null)
                {
                    return NotFound(new Response()
                    {
                        Status = false,
                        Message = $"This Review No Existing"
                    });

                }
                await _reviewRepository.DeleteReviewAsync(ReviewId);
                return Ok(new Response()
                {
                    Status = true,
                    Message = $"Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Status = false,
                    Message = $"An error occurred while Deleted Review: {ex.Message}"
                });
            }
        }

        [HttpGet("[action]/{ReviewId}")]
        public async Task<ActionResult<Review>> GetReviewById(int ReviewId)
        {
            try
            {

                var review = await _reviewRepository.GetReviewByIdAsync(ReviewId);

                if (review == null)
                {
                    return NotFound(new Response()
                    {
                        Status = false,
                        Message = $"This Review No Existing"
                    });
                }
                return Ok(review);
            }
            catch(Exception ex) 
            {
                    return NotFound(new Response()
                    {
                        Status = false,
                        Message = $"An error occurred while updating the review: {ex.Message}"
                    });
            }
        }

        [HttpGet("[action]/{PlaceId}")]
        public async Task<ActionResult<IEnumerable<ReviewsPlaceDTOs>>> GetAllReviewByPlaceId(int PlaceId)
        {
            try
            {

                var reviews = await _reviewRepository.GetAllReviewByPlaceIdAsync(PlaceId);

                if (reviews == null || !reviews.Any())
                {
                    return NotFound(new Response()
                    {
                        Status = false,
                        Message = $"No Review on this Place"
                    });
                }

                return Ok(reviews);
            }
            catch(Exception ex) 
            {
                return NotFound(new Response()
                {
                    Status = false,
                    Message = $"This Place Not Found: {ex.Message}"
                });
            }
        }


    }
}
