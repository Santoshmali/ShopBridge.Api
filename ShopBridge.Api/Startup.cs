using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using ShopBridge.Api.Configurations;
using ShopBridge.Api.Validators;
using ShopBridge.Data;
using ShopBridge.Data.Catalog;
using ShopBridge.Data.Context;
using ShopBridge.Data.Repositories;
using ShopBridge.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopBridgeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShopBridgeConnectionString")));
            //services.AddDbContext<ShopBridgeDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            
            // Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IProductRepository, ProductRepository>();

            //
            services.AddFeatureManagement();

            // Services
            services.AddTransient<IProductService, ProductService>();

            services.AddMvcCore(options => {
                options.Filters.Add(typeof(ValidateModelFilter));
            });

            services.AddAutoMapper(typeof(AutoMapperConfigurations));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopBridge.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopBridge.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
