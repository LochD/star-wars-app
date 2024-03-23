using StarWarsApp.Models;

namespace StarWarsApp.Repositories;

public class StarshipRepository
{
    private readonly StarWarsDbContext dbContext;
    
    public StarshipRepository(StarWarsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddStarship(StarShip starShip)
    {
        await this.dbContext.StarShip.AddAsync(starShip);
        await this.dbContext.SaveChangesAsync();
    }

    public StarShip GetStarshipByName(string starShipName)
    {
        return this.dbContext.StarShip.FirstOrDefault(starShip => starShip.Name == starShipName);
    }

    public async Task<StarShip> UpdateStarship(StarShip starShip)
    {
        var starShipBeforeUpdate = GetStarshipById(starShip.Id);
        starShipBeforeUpdate.Name = starShip.Name;
        await this.dbContext.SaveChangesAsync();
        return GetStarshipById(starShip.Id);
    }

    public async Task DeleteStarship(int starShipId)
    {
        var starShipToDelete = GetStarshipById(starShipId);
        dbContext.StarShip.Remove(starShipToDelete);
        await this.dbContext.SaveChangesAsync();
    }
    
    private StarShip GetStarshipById(int id)
    {
        return this.dbContext.StarShip.FirstOrDefault(starShip => starShip.Id == id);
    }
}