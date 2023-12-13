using System.ComponentModel.DataAnnotations;

namespace Tourism.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; } = null!;

        [Display(Name = "Read Status")]
        public bool ReadStatus { get; set; }

        public DateTime Date { get; set; }

        //public int UserId { get; set; }
        //public virtual User? User { get; set; }


    }
}
