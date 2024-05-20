using Microsoft.EntityFrameworkCore;
using API.Data;
using PFA_ProjectAPI.Repositories;
using PFA_ProjectAPI.Mappings;

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
builder.Services.AddScoped<IEventRepository, SQLEventRepository>();

//inject automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
