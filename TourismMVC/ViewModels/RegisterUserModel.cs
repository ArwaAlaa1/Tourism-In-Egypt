using System.ComponentModel.DataAnnotations;
using TourismMVC.Helpers;

namespace TourismMVC.ViewModels
{
    public class RegisterUserModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string FName { get; set; }
        [Required]
        [MaxLength(15)]
        public string LName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Location { get; set; }
       
       
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
		public IFormFile? PhotoFile { get; set; }
		//public string? ProfilePhotoURL { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }

        [Required]
        [UniqueUserName]
        public string UserName { get; set; }

        public string Email {  get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
		//public bool IsAgree { get; set; }


	}
}
