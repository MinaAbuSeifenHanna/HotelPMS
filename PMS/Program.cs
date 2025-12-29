using Microsoft.EntityFrameworkCore;
using PMS.Core.Domain.Interfaces;
using PMS.Core.Infrastructure.RepoIMP;
using PMS.Features.Guests.Application.Services;
using PMS.Features.Guests.Domain.IRepositories;
using PMS.Features.Guests.Infrastructure.RepositoriesIMP;
using PMS.Features.Reservations.Application.Services;
using PMS.Features.Reservations.Domain.IRepositories;
using PMS.Features.Reservations.Infrastructure.RepositoriesIMP;
using PMS.Features.Rooms.Application.Services;
using PMS.Features.Rooms.Domain.IRepositories;
using PMS.Features.Rooms.Infrastructure.RepositoriesIMP;
using PMS.Features.RoomServiceRequests.Application.Services;
using PMS.Features.RoomServiceRequests.Domain.IRepositories;
using PMS.Features.RoomServiceRequests.Infrastructure;
using PMS.Features.SPA;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DbContext
builder.Services.AddDbContext<PMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// me: add auto mapper
   builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// me:  :Generic Repository 
   builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// me. Repositories 
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IHousekeepingRepository, HousekeepingRepository>();


// me.Services
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IHousekeepingService, HousekeepingService>();

//SPA
builder.Services.AddSpaDependencies();

///////
// add ReservationService
//builder.Services.AddScoped<IReservationService, ReservationService>();

// add GuestService
//builder.Services.AddScoped<IGuestService, GuestService>();

//builder.Services.AddScoped<IHousekeepingRepository, HousekeepingRepository>();

//builder.Services.AddScoped<IHousekeepingService, HousekeepingService>();

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
