using System.ComponentModel.DataAnnotations;

namespace TourismMVC.ViewModels
{
    public class LoginUserModel
    {
      
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
