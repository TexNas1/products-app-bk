using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductsApp.Application.Products;
using ProductsApp.Application.Products.Services;
using ProductsApp.Domain.Repositories;
using ProductsApp.Infrastructure.Data;
using ProductsApp.Infrastructure.Repositories;

namespace ProductsApp.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure database context
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("ProductsDbConnection"));
            });

            // Configure repositories
            services.AddScoped<IProductRepository, ProductRepository>();

            // Configure services
            services.AddScoped<IProductService, ProductService>();

            // Configure Swagger/OpenAPI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
