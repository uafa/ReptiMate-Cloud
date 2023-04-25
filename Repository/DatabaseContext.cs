using Microsoft.EntityFrameworkCore;
using Model;


namespace Repository;

public class DatabaseContext : DbContext
{ 
    public DbSet<Measurements> Measurements { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("" +
                                 DatabaseCredentials.Host +
                                 DatabaseCredentials.Port +
                                 DatabaseCredentials.Database +
                                 DatabaseCredentials.Username +
                                 DatabaseCredentials.Password,
            options => options.UseAdminDatabase(DatabaseCredentials.AdminDatabase));    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurements>().HasKey(measurement => measurement.Id);
    }
}