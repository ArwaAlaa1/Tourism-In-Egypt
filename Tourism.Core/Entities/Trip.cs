namespace Tourism.Core.Entities
{
    public class Trip : BaseEntity
    {
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public virtual ICollection<Place> Places { get; set; } = new List<Place>();
    }

}
