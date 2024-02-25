namespace Tourism.Core.Entities
{
    public class User_Trip : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TripId { get; set;}
        public virtual Trip Trip { get; set;}
    }
}
