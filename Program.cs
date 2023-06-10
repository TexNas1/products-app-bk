using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductsApp.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using ProductsApp.Application.Products.Services;
using ProductsApp.Application.Products;
using ProductsApp.Domain.Repositories;
using ProductsApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register the database context.
builder.Services.AddDbContext<ProductDbContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Register the ProductService
builder.Services.AddScoped<IProductService, ProductService>();
// Register Swagger services.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
