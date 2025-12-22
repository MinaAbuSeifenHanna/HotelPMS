using Microsoft.EntityFrameworkCore;
using PMS.Domain.entity;
using System.Collections.Generic;
using System.Reflection.Emit;

public class PMSContext : DbContext
{
    public PMSContext(DbContextOptions<PMSContext> options) : base(options) { }

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        modelBuilder.Entity<Room>()
            .Property(r => r.PricePerNight)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Room>()
            .HasIndex(r => r.RoomNumber)
            .IsUnique();
        modelBuilder.Entity<Guest>()
        .HasIndex(g => g.NationalId)
        .IsUnique();

        modelBuilder.Entity<Reservation>()
            .Property(res => res.TotalAmount)
            .HasColumnType("decimal(18,2)");
    }
}