using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Entities
{
    public class Review : BaseEntity
    {
        //proprties

        [Required]
        public string? Message { get; set; }

        public float Rating { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public virtual User? User { get; set; }


        public int Placeid { get; set; }
        public virtual Place? Place { get; set; }




    }
}
