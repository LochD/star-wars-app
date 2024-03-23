using System.ComponentModel.DataAnnotations;

namespace StarWarsApp.Models;

public class StarShip
{
    
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public StarShip(string name)
    {
        this.Name = name;
    }
    
    public StarShip() { }
}