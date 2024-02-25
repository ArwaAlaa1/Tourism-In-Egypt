using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Location).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(12).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(11).IsRequired();

            builder.HasMany(x => x.Places)
                .WithMany(x => x.Users).UsingEntity<Favorite>();
                
            builder.ToTable("Users");

        }
    }
}
