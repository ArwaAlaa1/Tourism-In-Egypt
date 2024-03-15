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
        //public List<PhotoDTO> ConvertionPlace(List<PlacePhotos> Photos)
        //{
        //    List<PhotoDTO> dtoList = new List<PhotoDTO>();

        //    foreach (var placePhoto in Photos)
        //    {
        //        PhotoDTO dto = new PhotoDTO();
        //        dto.Id = placePhoto.Id;
        //        dto.Photo = $"{configuration["BaseUrl"]}/{placePhoto.Photo}";

        //        dtoList.Add(dto);
        //    }

        //    return dtoList;
        //}

      
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
    //public class PhotosResolve
    //{
    //    private readonly IConfiguration _configuration;
    //    private static string base1;

    //    public string Base
    //    {
    //        get { return base1; }
    //        set { base1 = _configuration["ApiBaseUrl"]; }
    //    }

    //    public PhotosResolve(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public static List<PhotoDTO> ConvertionCity(List<CityPhotos> Photos)
    //    {
    //        List<PhotoDTO> dtoList = new List<PhotoDTO>();

    //        foreach (var cityPhoto in Photos)
    //        {
    //            PhotoDTO dto = new PhotoDTO();
    //            dto.Id = cityPhoto.Id;
    //            dto.Photo = $"{base1}/{cityPhoto.Photo}";

    //            dtoList.Add(dto);
    //        }

    //        return dtoList;
    //    }

    //public static List<PhotoDTO> ConvertionPlace(List<PlacePhotos> Photos)
    //{
    //    List<PhotoDTO> dtoList = new List<PhotoDTO>();

    //    foreach (var placePhoto in Photos)
    //    {
    //        PhotoDTO dto = new PhotoDTO();
    //        dto.Id = placePhoto.Id;
    //        dto.Photo = $"{base1}/{placePhoto.Photo}";

    //        dtoList.Add(dto);
    //    }

    //    return dtoList;
    //}

    //    #region MyRegion
    //    //public static List<PhotoDTO> Convertion(List<T> Photos)
    //    //{
    //    //    List<PhotoDTO> dtoList = new List<PhotoDTO>();

    //    //    if (typeof(T) is CityPhotos)
    //    //    { 
    //    //      foreach (var cityPhoto in Photos)
    //    //      {
    //    //        PhotoDTO dto = new PhotoDTO();
    //    //        dto.Id = cityPhoto.Id;
    //    //        dto.Photo = $"{base1}/{cityPhoto.Photo}";

    //    //        dtoList.Add(dto);
    //    //      }

    //    //    }

    //    //    return dtoList;
    //    //}
    //    #endregion
    //}
}
