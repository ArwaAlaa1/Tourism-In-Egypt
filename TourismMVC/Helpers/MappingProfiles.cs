using AutoMapper;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
    public class MappingProfiles : Profile
    {

<<<<<<< HEAD
		public MappingProfiles()
		{
			CreateMap<PlaceViewModel,Place>().ReverseMap();
			CreateMap<City, CityViewModel>().ReverseMap();

			CreateMap<Category, CategoryViewModel>().ReverseMap();

			CreateMap<CityPhotosViewModel,CityPhotos>().ReverseMap();
=======
        public MappingProfiles()
        {
            CreateMap<PlaceViewModel, Place>().ReverseMap();
            CreateMap<CityPhotosViewModel, CityPhotos>().ReverseMap();
>>>>>>> acb63b4454099ac197828b42ab48096f1c5d2400
            CreateMap<PlacePhotoViewModel, PlacePhotos>().ReverseMap();
            CreateMap<Place_TripModel, Place_Trip>().ReverseMap();
            CreateMap<RoleViewModel, ApplicationRole>()
                .ForMember(AR => AR.Name, RV => RV.MapFrom(v => v.RoleName)).ReverseMap();

        }
    }
}
