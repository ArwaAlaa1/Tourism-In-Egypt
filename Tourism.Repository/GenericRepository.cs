﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TourismContext _context;

        public GenericRepository(TourismContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(CityPhotos))
            //{
            //    return (IEnumerable<T>) _context.CityPhotos.Include(c=>c.city);
            //}
            //else
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            
            return await _context.Set<T>().FindAsync(id);

        }

     

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
          
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
           
        }

        public CityPhotos GetCityPhAsync(int id)
        {
            return  _context.CityPhotos.Include("city").FirstOrDefault(x => x.Id == id);

        }
    }
}
