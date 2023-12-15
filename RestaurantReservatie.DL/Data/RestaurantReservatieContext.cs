using Microsoft.EntityFrameworkCore;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Data; 

public class RestaurantReservatieContext : DbContext {
    
    public DbSet<Customer_Data> Customers { get; set; }
    public DbSet<ContactInfo_Data> ContactInfos { get; set; }
    public DbSet<Location_Data> Locations { get; set; }
    public DbSet<Reservation_Data> Reservations { get; set; }
    public DbSet<Restaurant_Data> Restaurants { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Configuring the primary key for each entity
        modelBuilder.Entity<ContactInfo_Data>().HasKey(c => c.Id);
        modelBuilder.Entity<Customer_Data>().HasKey(c => c.Id);
        modelBuilder.Entity<Location_Data>().HasKey(l => l.Id);
        modelBuilder.Entity<Reservation_Data>().HasKey(r => r.ReservationID);
        modelBuilder.Entity<Restaurant_Data>().HasKey(r => r.Id);

        modelBuilder.Entity<Restaurant_Data>()
            .HasMany(r => r.Reservations)
            .WithOne(res => res.Restaurant)
            .HasForeignKey(res => res.RestaurantId);
    }


}