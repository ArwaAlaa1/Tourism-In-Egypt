using AutoMapper;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
	public class MappingProfiles :Profile
	{

		public MappingProfiles()
		{
			CreateMap<PlaceViewModel,Place>().ReverseMap();
			CreateMap<CityPhotosViewModel,CityPhotos>().ReverseMap();
            CreateMap<PlacePhotoViewModel, PlacePhotos>().ReverseMap();
            CreateMap<NotificationModel,Notification>().ReverseMap();
            CreateMap<Place_TripModel, Place_Trip>().ReverseMap();

        }
    }
}
