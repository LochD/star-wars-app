using System.ComponentModel.DataAnnotations;

namespace StarWarsApp.Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ICollection<Starship> Starships { get; set; } = new List<Starship>();

    public Person() { }
    
    public Person(string name)
    {
        this.Name = name;
    }
}