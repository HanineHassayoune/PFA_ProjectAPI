using Microsoft.EntityFrameworkCore;
using API.Data;
using PFA_ProjectAPI.Repositories;
using PFA_ProjectAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PFA_ProjectAPI.Data;
using Microsoft.AspNetCore.Identity;

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


// inject our Dbcontext class TBAuthDbContext in our application
builder.Services.AddDbContext<TBAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TBAuthConnectionString")));
//inject the repository 
builder.Services.AddScoped<IActivityRepository, SQLActivityRepository>();
builder.Services.AddScoped<IEventRepository, SQLEventRepository>();

//inject automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("TB")
    .AddEntityFrameworkStores<TBAuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit=false;
    options.Password.RequireLowercase=false;
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequireUppercase=false;
    options.Password.RequiredLength=6;
    options.Password.RequiredUniqueChars=1;
}
);
//inject authentication to the services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
