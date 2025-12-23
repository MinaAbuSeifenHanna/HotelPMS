using Microsoft.EntityFrameworkCore;
using PMS.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DbContext
builder.Services.AddDbContext<PMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// add Room services 

builder.Services.AddScoped<IRoomService, RoomService>();
// add ReservationService
builder.Services.AddScoped<IReservationService, ReservationService>();

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
