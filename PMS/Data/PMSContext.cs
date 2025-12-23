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
        //---- Guest Entity Configuration ----//
        modelBuilder.Entity<Guest>()
            .HasIndex(g => g.IdNumber)
            .IsUnique();
        modelBuilder.Entity<Guest>()
            .HasIndex(g => g.Email)
            .IsUnique();
        //---- Room Entity Configuration ----//
        modelBuilder.Entity<Room>()
            .Property(r => r.PricePerNight)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Room>()
            .HasIndex(r => r.RoomNumber)
            .IsUnique();
        //---- Reservation Entity Configuration ----//

        modelBuilder.Entity<Reservation>()
         .Property(r => r.TotalAmount)
         .HasPrecision(18, 2);

        modelBuilder.Entity<Reservation>()
            .Property(r => r.DepositAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Companion>()
        .HasOne(c => c.Reservation)      
        .WithMany(r => r.Companions)     
        .HasForeignKey(c => c.ReservationId) 
        .OnDelete(DeleteBehavior.Cascade); 
    }
}