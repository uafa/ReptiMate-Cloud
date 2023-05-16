using Microsoft.EntityFrameworkCore;
using Model;


namespace Repository;

public class DatabaseContext : DbContext
{
    public DbSet<Measurements>? Measurements { get; set; } = null!;
    public DbSet<TerrariumBoundaries>? TerrariumBoundaries { get; set; } = null!;
    public DbSet<TerrariumLimits>? TerrariumLimits { get; set; } = null!;
    public DbSet<Terrarium>? Terrarium { get; set; } = null!;
    public DbSet<Notification>? Notifications { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseNpgsql(
        //     $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                //     $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                //     $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                //     $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                //     $"Password={Environment.GetEnvironmentVariable("DB_PASS")}");
                
        optionsBuilder.UseNpgsql("" +
                                 DatabaseCredentials.Host +
                                 DatabaseCredentials.Port +
                                 DatabaseCredentials.Database +
                                 DatabaseCredentials.Username +
                                 DatabaseCredentials.Password);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurements>().HasKey(measurement => measurement.Id);
        modelBuilder.Entity<TerrariumBoundaries>().HasKey(terrariumBoundaries => terrariumBoundaries.Id);
        modelBuilder.Entity<TerrariumLimits>().HasKey(terrariumLimits => terrariumLimits.Id);
        modelBuilder.Entity<Terrarium>().HasKey(terrarium => terrarium.name);
        modelBuilder.Entity<Notification>().HasKey(notification => notification.Id);
        modelBuilder.Entity<Notification>().HasIndex(notification => notification.DateTime);
    }
}