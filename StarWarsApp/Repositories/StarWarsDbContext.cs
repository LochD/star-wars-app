using Microsoft.EntityFrameworkCore;
using StarWarsApp.Models;

namespace StarWarsApp.Repositories;

public class StarWarsDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Starship> StarShips { get; set; }
    
    public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options) : base(options)
    {
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseMySQL("server=localhost;database=starwarsdbtest;user=root;password=");
    // }
    //
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<StarShip>();
    // }
    //
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Blog>()
    //         .HasMany(e => e.Posts)
    //         .WithOne(e => e.Blog)
    //         .HasForeignKey(e => e.BlogId)
    //         .IsRequired();
    // }
    //
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Person>()
        //     .HasMany(e => e.StarShips)
        //     .WithOne(e => e.Person)
        //     .HasForeignKey(e => e.Person)
        //     .IsRequired();
        modelBuilder.Entity<Person>().OwnsMany(p => p.Starships);
    }
    
    //jeden blok ma wiele postow, relacje jeden do wielu jeden blog --> wiele postow czyli u mnie jedna osoba wiele statkow
}