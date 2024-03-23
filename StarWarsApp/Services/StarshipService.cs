using StarWarsApp.Clients;

namespace StarWarsApp.Services;

public class StarshipService
{
    private StarWarsClient starWarsClient;
    
    public StarshipService()
    {
        this.starWarsClient = new StarWarsClient();
    }

    public async Task<List<string>> GetPersonStarships(string starWarserName)
    {
        return await this.starWarsClient.GetStarWarserships(starWarserName);
    }
}