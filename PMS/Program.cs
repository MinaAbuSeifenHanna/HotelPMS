
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PMS.Application;
using PMS.Application.Services;
using PMS.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(
    typeof(PMS.Application.ModuleApplicationDependencies).Assembly
);

// Dependcy injection
builder.Services.AddApplicationDependencies().AddInfrastructureDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DbContext
builder.Services.AddDbContext<PMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// add Room services 

// builder.Services.AddScoped<IRoomService, RoomService>();
// // add ReservationService
// builder.Services.AddScoped<IReservationService, ReservationService>();

// add GuestService
builder.Services.AddScoped<IGuestService, GuestService>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
