namespace Tourism.Core.Entities
{
    public class UserFav : BaseEntity
    {

        public int FavoriteId { get; set; }
        public int UserId { get; set; }

        public int PlaceId { get; set; }
    }
}
