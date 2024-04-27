﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    internal class CategoryPhotoResolved : IValueResolver<Category, CategoryDTO, string>
    {
        private readonly IConfiguration configuration;

        public CategoryPhotoResolved(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Category source, CategoryDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImgUrl))
                return $"{configuration["ApiBaseUrl"]}/{source.ImgUrl}";

            return string.Empty ;
        }
    }
}
