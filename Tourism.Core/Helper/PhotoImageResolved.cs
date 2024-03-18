using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class PhotoImageResolved : IValueResolver<Place,PlaceDTO,IEnumerable<PhotoDTO>>
    {
        private readonly IConfiguration configuration;

        public PhotoImageResolved(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
      
        public IEnumerable<PhotoDTO> Resolve(Place source, PlaceDTO destination, IEnumerable<PhotoDTO> destMember, ResolutionContext context)
        {
            List<PhotoDTO> dtoList = new List<PhotoDTO>();

            foreach (var placePhoto in source.Photos)
            {
                PhotoDTO dto = new PhotoDTO();
                dto.Id = placePhoto.Id;
                dto.Photo = $"{configuration["ApiBaseUrl"]}/{placePhoto.Photo}";

                dtoList.Add(dto);
            }

            return dtoList;
        }
    }

}
