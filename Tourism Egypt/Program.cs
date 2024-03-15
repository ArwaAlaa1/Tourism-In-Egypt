using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using Tourism.Repository.Data;
using Tourism.Repository.Repository;
using Tourism.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tourism.Core.Helper;
using Microsoft.AspNetCore.Identity;

namespace Tourism_Egypt
{
    public class Program
    {
        public static  async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           
            #region Add services to the container.

            builder.Services.AddControllers();

            //dbcontext
            #region Container Services
            builder.Services.AddDbContext<TourismContext>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
            builder.Services.AddScoped(typeof(IPlaceRepository), typeof(PlaceRepository));

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            #endregion
            #region Identity Services
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
            config =>
            {
                config.Password.RequireUppercase = false;
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);


            })//configuration
                .AddEntityFrameworkStores<TourismContext>();

            //Authentication Schema
            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
                AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(builder.Configuration["JWT:Duration"]))
                }
                ) ;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin();
                }
                );
            });
            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion

            var app = builder.Build();


            #region Update DB

            //Explicitly
           using  var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var loggerfactury = services.GetRequiredService<ILoggerFactory>();
            var dbContext = services.GetRequiredService<TourismContext>();
            try
            {
              
                 await dbContext.Database.MigrateAsync();
            
            }
            catch (Exception ex)
            {
                var logger = loggerfactury.CreateLogger<Program>();//return to main 
                logger.LogError(ex, "Erro Occured during apply migration");

            }

            #endregion


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
               app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}