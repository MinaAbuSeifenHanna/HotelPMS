using Microsoft.EntityFrameworkCore;
using PMS.Features.Guests.Domain.Entities;
using PMS.Features.Reservations.Domain.Entities;
using PMS.Features.Rooms.Domain.Entities;
using PMS.Features.RoomServiceRequests.Domain.Entities;
using PMS.Features.SPA.SpaBookings.Domain.Entities;
using PMS.Features.SPA.SpaServices.Domain.Entities;
using PMS.Features.SPA.SpaTherapists.Domain.Entities;
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
    public DbSet<SpaService> SpaServices { get; set; }
    public DbSet<SpaRoom> SpaRooms { get; set; }
    public DbSet<SpaBooking> SpaBookings { get; set; }
    public DbSet<SpaTherapist> SpaTherapists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
        //---- SPA Module Configuration ----//
        modelBuilder.Entity<SpaBooking>(entity =>
     {
         entity.HasOne(b => b.Guest)
               .WithMany()
               .HasForeignKey(b => b.GuestId)
               .OnDelete(DeleteBehavior.Restrict);

         entity.HasOne(b => b.SpaService)
               .WithMany(s => s.SpaBookings)
               .HasForeignKey(b => b.SpaServiceId)
               .OnDelete(DeleteBehavior.Restrict);

         entity.HasOne(b => b.Therapist)
               .WithMany(t => t.SpaBookings)
               .HasForeignKey(b => b.TherapistId)
               .OnDelete(DeleteBehavior.Restrict);

         entity.HasOne(b => b.Room)
               .WithMany(r => r.SpaBookings)
               .HasForeignKey(b => b.RoomId)
               .OnDelete(DeleteBehavior.Restrict);
     });

    }
}