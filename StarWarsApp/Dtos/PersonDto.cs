namespace StarWarsApp.Dtos;

public class PersonDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<string> Starships { get; set; }
}