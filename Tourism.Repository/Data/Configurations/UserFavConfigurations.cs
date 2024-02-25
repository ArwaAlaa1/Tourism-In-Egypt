using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class UserFavConfigurations : IEntityTypeConfiguration<UserFav>
    {
        public void Configure(EntityTypeBuilder<UserFav> builder)
        {
            builder.HasKey(x => x.Id);
 
        }

    }
}
