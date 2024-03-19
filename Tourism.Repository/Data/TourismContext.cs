﻿ using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class TourismContext : IdentityDbContext<ApplicationUser , ApplicationRole ,int>
    {

        public TourismContext()
        {
        }


        public TourismContext(DbContextOptions<TourismContext> options)
            : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server = ARWA-ALAA\\ARWAALAA; Database = Tourism ; Trusted_Connection = true");


            optionsBuilder.UseSqlServer("Data Source=SQL5109.site4now.net;Initial Catalog=db_aa6718_tourism;User Id=db_aa6718_tourism_admin;Password=DotNetDev1;MultipleActiveResultSets=true;");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);


        //    optionsBuilder.UseSqlServer("Server = DESKTOP-9IISLS5; Database = Tourism ; Trusted_Connection = true ;MultipleActiveResultSets=true ");
        //    optionsBuilder.UseSqlServer("Data Source=SQL5109.site4now.net;Initial Catalog=db_aa6718_tourism;User Id=db_aa6718_tourism_admin;Password=DotNetDev1");

        //}
        public DbSet<Trip> Trips { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
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

        
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(t => new {t.ProviderKey , t.LoginProvider });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(t => new { t.RoleId, t.UserId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
