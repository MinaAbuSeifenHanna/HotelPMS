using Microsoft.EntityFrameworkCore;
using PMS.Features.Guests.Domain.Entities;
using PMS.Features.Reservations.Domain.Entities;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

public class PMSContext : DbContext
{

    public PMSContext(DbContextOptions<PMSContext> options) : base(options) { }

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
  
    public DbSet<Companion> Companions { get; set; }

    public DbSet<RoomServiceRequest> RoomServiceRequests { get; set; }

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


        modelBuilder.Entity<Reservation>()
       .HasOne(r => r.Room)
       .WithMany(room => room.Reservations)
       .HasForeignKey(r => r.RoomId)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
        .HasOne(r => r.Guest)
        .WithMany(g => g.Reservations)
        .HasForeignKey(r => r.GuestId)
        .OnDelete(DeleteBehavior.Restrict);


        //---- Housekeeping Module Configuration ----//

        modelBuilder.Entity<RoomServiceRequest>(entity =>
        {
            entity.HasOne(s => s.Room)
                  .WithMany(r => r.ServiceRequests)
                  .HasForeignKey(s => s.RoomId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(s => s.ServiceType).IsRequired().HasMaxLength(50);
            entity.Property(s => s.Description).HasMaxLength(500);
        });
    }
}