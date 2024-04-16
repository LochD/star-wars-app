using System.ComponentModel.DataAnnotations;

namespace StarWarsApp.Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ICollection<StarShip> StarShips { get; set; } = new List<StarShip>();

    public Person() { }
    
    public Person(string name)
    {
        this.Name = name;
    }
}