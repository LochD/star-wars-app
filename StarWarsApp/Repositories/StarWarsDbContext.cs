using Microsoft.EntityFrameworkCore;
using StarWarsApp.Models;

namespace StarWarsApp.Repositories;

public class StarWarsDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<StarShip> StarShip { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=starwarsdbtest;user=root;password=");
    }
    
}