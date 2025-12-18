using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SimpleCatalog.API.Middleware;
using SimpleCatalog.Application.Interfaces;
using SimpleCatalog.Application.Mappings;
using SimpleCatalog.Application.Services;
using SimpleCatalog.Application.Validators;
using SimpleCatalog.Infrastructure.Data;
using SimpleCatalog.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
builder.Services.AddAutoMapper(cfg => { }, typeof(ProductProfile).Assembly);


builder.Services.AddControllers();
    
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

// Register validators from assembly 
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

//Database:
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

//Register Services:
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SupplierService>();


var app = builder.Build();

//  <== MiddleWare Setup Here:

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
