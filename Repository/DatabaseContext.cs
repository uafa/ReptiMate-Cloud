using Microsoft.EntityFrameworkCore;
using Model;


namespace Repository;

public class DatabaseContext : DbContext
{
    public DbSet<Measurements> Measurements { get; set; }
    public DbSet<TerrariumBoundaries> Boundaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*optionsBuilder.UseNpgsql(
            $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
            $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
            $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
            $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
            $"Password={Environment.GetEnvironmentVariable("DB_PASS")}",
            options => options.UseAdminDatabase(Environment.GetEnvironmentVariable("DB_ADMIN_DATABASE")));*/
        
        optionsBuilder.UseNpgsql("" +
                                 DatabaseCredentials.Host +
                                 DatabaseCredentials.Port +
                                 DatabaseCredentials.Database +
                                 DatabaseCredentials.Username +
                                 DatabaseCredentials.Password,
            options => options.UseAdminDatabase(DatabaseCredentials.AdminDatabase));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurements>().HasKey(measurement => measurement.Id);
        modelBuilder.Entity<TerrariumBoundaries>().HasKey(terrariumBoundaries => terrariumBoundaries.Id);
    }
}