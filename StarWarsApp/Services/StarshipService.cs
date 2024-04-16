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

    public Task<int> CreateStarShip(StarShip starShip)
    {
        return this.starShipRepository.CreateStarShip(starShip);
    }
}