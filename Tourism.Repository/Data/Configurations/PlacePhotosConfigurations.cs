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
    public class PlacePhotosConfigurations:IEntityTypeConfiguration<PlacePhotos>
    {

        public void Configure(EntityTypeBuilder<PlacePhotos> builder)
        {
            

            builder.HasOne<Place>(PP => PP.Place)
                  .WithMany(p=>p.Photos)
                  .HasForeignKey(PP => PP.PlaceId);

            builder.HasKey(PP => PP.Id);
        }
    }
}
