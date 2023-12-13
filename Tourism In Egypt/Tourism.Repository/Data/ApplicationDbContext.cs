using Microsoft.EntityFrameworkCore;
using Tourism.Models;

namespace Talabat.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }

    }
}
