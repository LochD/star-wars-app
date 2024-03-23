using System.ComponentModel.DataAnnotations;

namespace StarWarsApp.Models;

public class Person
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }    
    public List<string> Starships { get; set; }

    public Person() { }
    
    public Person(string name, string surname)
    {
        this.Name = name;
        this.Surname = surname;
    }
}