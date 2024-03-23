using StarWarsApp.Clients;

namespace StarWarsApp.Services;

public class PersonService
{
    private StarWarsClient starWarsClient;
    
    public PersonService()
    {
        this.starWarsClient = new StarWarsClient();
    }

    public async Task<bool> IsPersonExistOnStarWars(string personName)
    {
        var starWarsersNames = await this.starWarsClient.GetStarWarsersName();

        foreach (var name in starWarsersNames)
        {
            var equals = name.Equals(personName);
            if (equals)
            {
                return true;
            }
        }

        return false;
    }
    
    public async void AssignStarShipsToPerson(string personName)
    {
        var starWarsersNames = await this.starWarsClient.GetStarWarsersName();
    }
}