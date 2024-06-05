using System.Reflection;
using Assignment.Application.Common.Interfaces;
using Assignment.Domain.Entities;
using Assignment.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Country> Countries { get; set; }

    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Country>().HasData(
            new Country { CountryID = 1, CountryName = "USA" },
            new Country { CountryID = 2, CountryName = "Canada" },
            new Country { CountryID = 3, CountryName = "Mexico" }
        );

        builder.Entity<City>().HasData(
            new City { CityID = 1, CityName = "New York", CountryID = 1, },
            new City { CityID = 2, CityName = "Los Angeles", CountryID = 1 },
            new City { CityID = 3, CityName = "Toronto", CountryID = 2 },
            new City { CityID = 4, CityName = "Vancouver", CountryID = 2 },
            new City { CityID = 5, CityName = "Mexico City", CountryID = 3 },
            new City { CityID = 6, CityName = "Guadalajara", CountryID = 3 }
        );

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
