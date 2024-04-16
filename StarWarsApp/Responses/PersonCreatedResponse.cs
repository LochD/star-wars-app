using StarWarsApp.Models;

namespace StarWarsApp.Responses;

public class PersonCreatedResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Starship> StarShips { get; set; } = new List<Starship>();

    public PersonCreatedResponse(Person person)
    {
        this.Id = person.Id;
        this.Name = person.Name;
        this.StarShips = person.Starships;
    }
}