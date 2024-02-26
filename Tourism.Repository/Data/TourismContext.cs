using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data
{
    public class TourismContext : DbContext
    {
        public TourismContext() : base()
        {

        }
        public TourismContext(DbContextOptions<TourismContext> options)
            : base(options)
        {
            
        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CityPhotos> CityPhotos { get; set; }
        public DbSet<PlacePhotos> PlacePhotos { get; set; }
        public DbSet<User_Trip> User_Trips { get; set; }
        public DbSet<Place_Trip> Place_Trips { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<UserFav> UserFavs { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
           optionsBuilder.UseSqlServer(" Server = ARWA-ALAA\\ARWAALAA; Database = Tourism ; Trusted_Connection = true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
