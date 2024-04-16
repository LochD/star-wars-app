using System.ComponentModel.DataAnnotations;

namespace StarWarsApp.Models;

public class Starship
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    // [Required]
    // public int OwnerId { get; set; }

    // public Person Person { get; set; }

    public Starship(string name)
    {
        this.Name = name;
    }
    
    public Starship() { }
}