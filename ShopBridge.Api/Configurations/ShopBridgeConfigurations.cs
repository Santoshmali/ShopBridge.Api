using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShopBridge.Api.Validators;
using ShopBridge.Core.DataModels;
using ShopBridge.Data.Catalog;
using ShopBridge.Data.Repositories;
using ShopBridge.Data.Repositories.Users;
using ShopBridge.Services.Catalog;
using ShopBridge.Services.Users;
using System;
using System.Text;

namespace ShopBridge.Api.Configurations
{
    public static class ShopBridgeConfigurations
    {
        public static void AddShopBridgeServices(this IServiceCollection services, IConfiguration Configuration)
        {
            /*
            AddTransient: Transient objects are always different; a new instance is provided to every controller and every service.
            AddScoped: Scoped objects are the same within a request, but different across different requests.
            AddSingleton: Singleton objects are the same for every object and every request. 
            */

            // Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // Services
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();

            // For model validations
            services.AddMvcCore()
                .AddDataAnnotations()
                .AddMvcOptions(opt =>
                    opt.Filters.Add<ValidateModelFilter>());

            // Automapper
            services.AddAutoMapper(typeof(AutoMapperConfigurations));

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

        }
    }
}
