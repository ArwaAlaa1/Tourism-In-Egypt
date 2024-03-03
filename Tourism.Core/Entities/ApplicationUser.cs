using Microsoft.AspNetCore.Identity;

namespace Tourism.Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Location { get; set; }
        public string DisplayName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? ProfilePhotoURL { get; set; }
        //public bool  IsAgree { get; set; }
        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
        public virtual ICollection<Place> Places { get; set; } = new List<Place>();


    }

 
}
