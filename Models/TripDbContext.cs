using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Models;

public class TripDbContext : DbContext
{
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ClientTrip> ClientTrips { get; set; }
    public DbSet<CountryTrip> CountryTrips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasKey(c => c.IdClient);
        modelBuilder.Entity<Trip>().HasKey(t => t.IdTrip);
        modelBuilder.Entity<Country>().HasKey(c => c.IdCountry);

        // Konfiguracja kluczy złożonych
        modelBuilder.Entity<ClientTrip>()
            .HasKey(ct => new { ct.IdClient, ct.IdTrip });

        modelBuilder.Entity<CountryTrip>()
            .HasKey(ct => new { ct.IdCountry, ct.IdTrip });
    }
}