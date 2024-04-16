using StarWarsApp.Clients;
using StarWarsApp.Models;
using StarWarsApp.Repositories;

namespace StarWarsApp.Services;

public class StarshipService
{
    private readonly StarWarsDbContext dbContext;
    private StarshipRepository starShipRepository;

    public StarshipService(StarWarsDbContext dbContext)
    {
        this.starShipRepository = new StarshipRepository(dbContext);
    }

    public Task<int> CreateStarship(Starship starship)
    {
        return this.starShipRepository.CreateStarShip(starship);
    }
    
    public Starship GetStarshipByName(string starShipName)
    {
        return this.starShipRepository.GetStarshipByName(starShipName);
    }
    
    public async Task<Starship> UpdateStarship(Starship starship)
    {
        return await this.starShipRepository.UpdateStarship(starship);
    }
    
    public async Task DeleteStarship(int starShipId)
    {
        await this.starShipRepository.DeleteStarship(starShipId);
    }    
}