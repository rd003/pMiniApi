using Microsoft.EntityFrameworkCore;
using ProductMiniFullstackApi.Models.Domain;
using ProductMiniFullstackApi.Repositories.Abstract;
using ProductMiniFullstackApi.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>(); //resolving our dependencies
builder.Services.AddTransient<IProductRepository, ProductRepository>(); //resolving our dependencies
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(
                options =>
                options.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
