using StarWarsApp.Models;

namespace StarWarsApp.Repositories;

public class StarshipRepository
{
    private readonly StarWarsDbContext dbContext;
    
    public StarshipRepository(StarWarsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateStarShip(Starship starship)
    {
        await this.dbContext.StarShips.AddAsync(starship);
        await this.dbContext.SaveChangesAsync();
        return starship.Id;
    }
    
    public async Task<List<int>> CreateStarShips(List<Starship> starShips)
    {
        List<int> starShipsIds = new List<int>();
        foreach (var starShip in starShips)
        {
            var id = await CreateStarShip(starShip);
            starShipsIds.Add(id);
        }

        return starShipsIds;
    }

    public Starship GetStarshipByName(string starShipName)
    {
        return this.dbContext.StarShips.FirstOrDefault(starShip => starShip.Name == starShipName) ?? throw new InvalidOperationException();
    }

    public async Task<Starship> UpdateStarship(Starship starship)
    {
        var starShipBeforeUpdate = GetStarshipById(starship.Id);
        starShipBeforeUpdate.Name = starship.Name;
        await this.dbContext.SaveChangesAsync();
        return GetStarshipById(starship.Id);
    }

    public async Task DeleteStarship(int starShipId)
    {
        var starShipToDelete = GetStarshipById(starShipId);
        dbContext.StarShips.Remove(starShipToDelete);
        await this.dbContext.SaveChangesAsync();
    }    
    
    private Starship GetStarshipById(int id)
    {
        return this.dbContext.StarShips.FirstOrDefault(starShip => starShip.Id == id);
    }
}