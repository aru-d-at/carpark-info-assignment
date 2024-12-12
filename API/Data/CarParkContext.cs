using Microsoft.EntityFrameworkCore;
using CarParkInfo.API.Models;


namespace CarParkInfo.API.Data;


public class CarParkContext : DbContext
{
    public DbSet<CarPark> CarParks { get; set; } = null!;
    public DbSet<ParkingDetail> ParkingDetails { get; set; } = null!;
    public DbSet<UserFavorite> UserFavorites { get; set; } = null!;

    public CarParkContext(DbContextOptions<CarParkContext> options)
        : base(options)
    {
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=carpark.db");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarPark>()
            .HasIndex(c => c.CarParkType)
            .HasDatabaseName("idx_carpark_type");

        modelBuilder.Entity<CarPark>()
            .HasIndex(c => c.TypeOfParkingSystem)
            .HasDatabaseName("idx_parking_system");

        modelBuilder.Entity<ParkingDetail>()
            .HasIndex(p => p.FreeParking)
            .HasDatabaseName("idx_free_parking");

        modelBuilder.Entity<ParkingDetail>()
            .HasIndex(p => p.NightParking)
            .HasDatabaseName("idx_night_parking");
    }
}
