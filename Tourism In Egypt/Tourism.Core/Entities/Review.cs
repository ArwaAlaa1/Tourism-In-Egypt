
using System.ComponentModel.DataAnnotations;

namespace Tourism.Models
{
    public class Review
    {
        //proprties
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Message { get; set; }

        public float Rating { get; set; }

        public DateTime Time { get; set; }
        // public int UserId { get; set; }
        // public virtual User? User { get; set; }


        // public int Placeid { get; set; }
        // public virtual Place? Place { get; set; }
    }
}
