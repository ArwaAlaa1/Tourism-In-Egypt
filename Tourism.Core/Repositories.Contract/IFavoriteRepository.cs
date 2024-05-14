using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IFavoriteRepository
    {
        Task<Favorite> AddFavorite(Favorite userFav);
        Task DeletePlaceFromFavorite(int id);
        Task<IEnumerable<Favorite>> GetAllFavoriteByUserIdAsync(int UserId);
    }
}
