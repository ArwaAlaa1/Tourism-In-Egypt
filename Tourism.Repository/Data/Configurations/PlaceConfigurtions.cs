using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class PlaceConfigurtions : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.Location).IsRequired();
            builder.Property(P => P.Rating).IsRequired();
            builder.Property(P => P.Phone).IsRequired();
            builder.Property(P => P.Link).IsRequired();

            //builder.HasOne(P => P.Category)
            //       .WithMany()
            //       .HasForeignKey(P => P.CategoryId);

            //builder.HasOne(P => P.City)
            //       .WithMany()
            //       .HasForeignKey(P => P.CityId);
            
        }
    }
}
