using FoneApi.Data;
using FoneApi.Service.Interface;
using FoneApi.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using static IdentityModel.ClaimComparer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Sql Connection
builder.Services.AddDbContext<FoneDb>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FoneConn")));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion



#region Automapper configuration
builder.Services.AddAutoMapper(typeof(Program));
#endregion

#region Bussiness logic service
builder.Services.AddScoped<IFoneService, FoneService>();
#endregion



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
