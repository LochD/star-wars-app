using System.Text.Json;
using StarWarsApp.Responses;

namespace StarWarsApp.Clients
{
    public class StarWarsClient
    {
        private readonly HttpClient httpClient;

        public StarWarsClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<List<string>> GetStarWarsersName()
        {
            var response = await httpClient.GetAsync("https://swapi.dev/api/people");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var starWarsResponse = JsonSerializer.Deserialize<StarWarsResponse>(responseBody);

            if (starWarsResponse?.results != null)
            {
                List<string> listWithStarWarsersNames = starWarsResponse
                    .results
                    .Select(character => character.Name)
                    .ToList();

                return listWithStarWarsersNames;
            }

            return new List<string>();
        }

        public async Task<List<string>> GetStarWarserShipsByPersonName(string starWarserName)
        {
            var response = await httpClient.GetAsync("https://swapi.dev/api/people");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var starWarsResponse = JsonSerializer.Deserialize<StarWarsResponse>(responseBody);

            var characterWithNameFromParameter =
                starWarsResponse.results.
                    Where(character => character.Name == starWarserName)
                    .FirstOrDefault();
            var starShipsName = characterWithNameFromParameter.Starships;
            
            return starShipsName;
        }

        public async Task<bool> IsPersonExist(string personName)
        {
            var starWarsersNames = await GetStarWarsersName();
            return starWarsersNames.Select(name => name.Equals(personName)).Any(equals => equals);
        }
    }
}