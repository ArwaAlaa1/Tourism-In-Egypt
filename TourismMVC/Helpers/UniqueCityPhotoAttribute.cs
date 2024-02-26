using System.ComponentModel.DataAnnotations;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
    public class UniqueCityPhotoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var CityPh = (CityPhotosViewModel)validationContext.ObjectInstance;

            TourismContext context = new TourismContext();

            if (value != null && CityPh.Photo != null)
            {


                int Cityphvalue = (int)value;

                var CityPhoto = context.CityPhotos.Where(x => x.CityId == Cityphvalue && x.Photo == CityPh.Photo).FirstOrDefault();
                if (CityPhoto == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Choose Unique City & Photo");
                }



            }

            else
            {
                return new ValidationResult("The City & Photo is required");
            }

        }

    }
}
