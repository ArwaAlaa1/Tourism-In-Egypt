using AutoMapper;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<Place, PlaceDTO>().ForMember(b => b.Category, o => o.MapFrom(b => b.Category.Name))
                .ForMember(t => t.City, o => o.MapFrom(t => t.City.Name))
               .ForMember(p => p.Photos, o => o.MapFrom<PhotoPlaceResolved>());

            CreateMap<Category, CategoryDTO>().ForMember(i =>i.ImgUrl,i=>i.MapFrom<CategoryPhotoResolved>());

            CreateMap<City, CityDTO>()
              .ForMember(c => c.cityPhotos, o => o.MapFrom<PhotoCityResolved>());

            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Review, AddReviewDTO>().ReverseMap();
        }

    }
}
