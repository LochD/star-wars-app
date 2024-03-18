using System.Text.Json.Serialization;

namespace StarWarsApp.Responses
{
    public class StarWarsersResponse
    {
        public List<Character> results { get; set; }
    }

    public class Character
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("starships")]
        public string Starships { get; set; }
    }
}