using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class FavoriteController : BaseApiController
    {
        public readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteController(IMapper mapper, IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(FavoriteDTO favorite)
        {
            try
            {
                var mappedFavorite = _mapper.Map<FavoriteDTO, Favorite>(favorite);

                await _favoriteRepository.AddFavorite(mappedFavorite);

                return Ok("This Place Added To Your WishList");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(FavoriteDTO favorite)
        {
            try
            {
                var mappedFavorite = _mapper.Map<FavoriteDTO, Favorite>(favorite);

                await _favoriteRepository.DeleteFavorite(mappedFavorite);

                return Ok("This Place Removed From Your WishList");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int FavoriteId)
        {
            try
            {

                await _favoriteRepository.DeletePlaceFromFavorite(FavoriteId);

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetAllFavoriteByUserId(int UserId)
        {
            try
            {

                var Favorite = await _favoriteRepository.GetAllFavoriteByUserIdAsync(UserId);

                if (Favorite == null || !Favorite.Any())
                {
                    return NotFound("No Favorite");
                }

                return Ok(Favorite);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
