using Microsoft.EntityFrameworkCore;
using RestaurantReservatie.DL.Models;

namespace RestaurantReservatie.DL.Data;

public class RestaurantReservatieContext : DbContext {
    private string _connectionString;
    public DbSet<Customer_Data> Customer { get; set; }
    public DbSet<Location_Data> Location { get; set; }
    public DbSet<Reservation_Data> Reservation { get; set; }
    public DbSet<Restaurant_Data> Restaurant { get; set; }
    public DbSet<Table_Data> Table { get; set; }

    public RestaurantReservatieContext(string connectionString) {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
            relation.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }
}