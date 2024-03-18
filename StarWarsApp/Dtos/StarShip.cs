namespace StarWarsApp.Dtos;

public class StarShip
{
    public string Name { get; set; }

    public StarShip(string name)
    {
        this.Name = name;
    }
}