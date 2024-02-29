using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Entities
{
    public class Notification : BaseEntity
    {

        [Required]
        public string Message { get; set; } = null!;

        [Display(Name = "Read Status")]
        public bool ReadStatus { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
}
