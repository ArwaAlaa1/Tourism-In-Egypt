using Microsoft.EntityFrameworkCore;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using Tourism.Repository.Data;
using Tourism.Repository.Repository;

namespace Tourism_Egypt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            #region Add services to the container.

            builder.Services.AddControllers();

            //dbcontext
            builder.Services.AddDbContext<TourismContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



          

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}