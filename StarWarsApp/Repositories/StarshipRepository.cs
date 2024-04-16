using StarWarsApp.Models;

namespace StarWarsApp.Repositories;

public class StarshipRepository
{
    private readonly StarWarsDbContext dbContext;
    
    public StarshipRepository(StarWarsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateStarShip(StarShip starShip)
    {
        await this.dbContext.StarShips.AddAsync(starShip);
        await this.dbContext.SaveChangesAsync();
        return starShip.Id;
    }
    
    public async Task<List<int>> CreateStarShips(List<StarShip> starShips)
    {
        List<int> starShipsIds = new List<int>();
        foreach (var starShip in starShips)
        {
            var id = await CreateStarShip(starShip);
            starShipsIds.Add(id);
        }

        return starShipsIds;
    }

    public StarShip GetStarshipByName(string starShipName)
    {
        return this.dbContext.StarShips.FirstOrDefault(starShip => starShip.Name == starShipName);
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
        dbContext.StarShips.Remove(starShipToDelete);
        await this.dbContext.SaveChangesAsync();
    }    
    
    private StarShip GetStarshipById(int id)
    {
        return this.dbContext.StarShips.FirstOrDefault(starShip => starShip.Id == id);
    }
}