using System.Text.Json;
using StarWarsApp.Responses;

namespace StarWarsApp.Clients
{
    public class StarWarsClient
    {
        private readonly HttpClient httpClient;

        public StarWarsClient()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<string>> GetStarWarsersName()
        {
            var response = await httpClient.GetAsync("https://swapi.dev/api/people");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var starWarsers = JsonSerializer.Deserialize<StarWarsersResponse>(responseBody);
            
            if (starWarsers?.results != null)
            {
                List<string> listWithStarWarsersNames = starWarsers
                    .results
                    .Select(character => character.Name)
                    .ToList();

                return listWithStarWarsersNames;
            }
            else
            {
                return new List<string>();
            }
        }
    }
}