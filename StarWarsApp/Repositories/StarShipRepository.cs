using StarWarsApp.Models;

namespace StarWarsApp.Repositories;

public class StarShipRepository
{
    private readonly StarWarsDbContext dbContext;
    
    public StarShipRepository(StarWarsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddStarShip(StarShip starShip)
    {
        await this.dbContext.StarShip.AddAsync(starShip);
        await this.dbContext.SaveChangesAsync();
    }

    public StarShip GetStarShipByName(string starShipName)
    {
        return this.dbContext.StarShip.FirstOrDefault(starShip => starShip.Name == starShipName);
    }

    public async Task<StarShip> UpdateStarShip(StarShip starShip)
    {
        var starShipBeforeUpdate = GetStarShipById(starShip.Id);
        starShipBeforeUpdate.Name = starShip.Name;
        await this.dbContext.SaveChangesAsync();
        return GetStarShipById(starShip.Id);
    }

    public async Task DeleteStarShip(int starShipId)
    {
        var starShipToDelete = GetStarShipById(starShipId);
        dbContext.StarShip.Remove(starShipToDelete);
        await this.dbContext.SaveChangesAsync();
    }
    
    private StarShip GetStarShipById(int id)
    {
        return this.dbContext.StarShip.FirstOrDefault(starShip => starShip.Id == id);
    }
}