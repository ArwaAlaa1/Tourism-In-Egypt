using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tourism.Core.Entities;
using Tourism.Core.Helper;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using Tourism.Repository.Data;
using Tourism.Repository.Repository;
using Tourism.Service;

namespace Tourism_Egypt
{
    public class Program
    {
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            builder.Services.AddScoped(typeof(IReviewRepository), typeof(ReviewRepository));
        
            builder.Services.AddAutoMapper(typeof(MapperConfig));

            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            builder.Services.AddScoped<IEmailService, EmailService>();
            //builder.Services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            //})
            //    .AddGoogle(options=>
            //    {
            //        IConfigurationSection GoogleAuthSec = builder.Configuration.GetSection("Authentication:Google");
            //       options.ClientId=GoogleAuthSec["ClientId"];
            //        options.ClientSecret = GoogleAuthSec["ClientSecret"];


            //    });
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
                .AddEntityFrameworkStores<TourismContext>()
                .AddDefaultTokenProviders();

            //Authentication Schema
            builder.Services.AddAuthentication(options =>
            {
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
                ).AddGoogle(options =>
                {
                    IConfigurationSection GoogleAuthSec = builder.Configuration.GetSection("Authentication:Google");
                    options.ClientId = GoogleAuthSec["ClientId"];
                    options.ClientSecret = GoogleAuthSec["ClientSecret"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]; ;

                    options.CallbackPath = "/auth/google-callback";

                }); ;

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
            using var scope = app.Services.CreateScope();
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
<<<<<<< HEAD
            app.UseCors("MyPolicy");
=======
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger/index.html");
                    return;
                }
                await next();
            });
>>>>>>> 7edf03a0e8c962282a46de23c891172b8206f9ab
            app.UseHttpsRedirection();

          
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}