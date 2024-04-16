using StarWarsApp.Models;

namespace StarWarsApp.Responses;

public class PersonCreatedResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public ICollection<StarShip> StarShips { get; set; } = new List<StarShip>();

    public PersonCreatedResponse(Person person)
    {
        this.Id = person.Id;
        this.Name = person.Name;
        this.Surname = person.Surname;
        this.StarShips = person.StarShips;
    }
}