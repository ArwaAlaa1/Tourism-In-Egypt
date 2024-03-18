using AutoMapper;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class PlaceResolved : IValueResolver<Category, CategoryDTO, IEnumerable<PlaceDTO>>
    {
 

        public IEnumerable<PlaceDTO> Resolve(Category source, CategoryDTO destination, IEnumerable<PlaceDTO> destMember, ResolutionContext context)
        {
            List<PlaceDTO> dtoList = new List<PlaceDTO>();

            foreach (var place in source.Places)
            {
                PlaceDTO dto = new PlaceDTO();
                dto.Id = place.Id;
                dto.Name = place.Name;
                dto.Description = place.Description;
                dto.Location = place.Location;
                dto.Category = place.Category.Name;
                dto.City = place.City.Name;
                dto.Rating = place.Rating;
                dto.Link = place.Link;
                dto.Phone = place.Phone;
                //PhotoDTO dto2 = new PhotoDTO();
                //dto2.Id = placePhoto.Id;
                //dto2.Photo = $"{configuration["ApiBaseUrl"]}/{placePhoto.Photo}";


                dtoList.Add(dto);
            }

            return dtoList;
        }
    }
}