using System.Text;
using Backend.Models;
using Backend.Repo;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//for sql server
builder.Services.AddDbContext<LE_dbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LE_AppCon")!);
});
//////////////////////////////////////////For JWT ////////////////////////////
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
      {
          options.SaveToken = true;
          options.RequireHttpsMetadata = false;
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              ClockSkew = TimeSpan.Zero,
              ValidateIssuerSigningKey = true,
              ValidIssuer = builder.Configuration["Jwt:Issuer"],
              ValidAudience = builder.Configuration["Jwt:Audience"],
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
          };

      });


/////////////////////////////////////////////////////////////////////

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());


builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddScoped<IPortfolio, PortfolioServices>();
builder.Services.AddScoped<IProject, ProjectServices>();
builder.Services.AddScoped<IReview, ReviewServices>();
builder.Services.AddScoped<ILogin, LoginServices>();


builder.Services.AddCors(c =>
{
c.AddPolicy("AllowAllOrigin", options => options.AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAllOrigin");

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
