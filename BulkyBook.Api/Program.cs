using BulkyBook.Api.Data;
using Microsoft.EntityFrameworkCore;
using BulkyBook.Api.Service;
using BulkyBook.Api.MiddleWares;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CategoryDbContext>(options => options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection")

    ));
builder.Services.AddScoped<ICategoryService, CategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder.WithOrigins("http://localhost:7262")
.AllowAnyHeader()
.AllowAnyMethod());

app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();


app.Run();
