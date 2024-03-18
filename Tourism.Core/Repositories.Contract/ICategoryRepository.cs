﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
       //Task<List<Place>> GetAllPlacesBySpecificCategory(int id);
    }
}
