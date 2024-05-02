using Microsoft.EntityFrameworkCore;
using API.Data;
using PFA_ProjectAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dependency injection
// inject our Dbcontext class TBDbContext in our application
builder.Services.AddDbContext<TBDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TBConnectionString")));


//inject the repository 
builder.Services.AddScoped<IActivityRepository, SQLActivityRepository>(); 

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
