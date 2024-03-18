using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Dtos;
using StarWarsApp.Repositories;

namespace StarWarsApp.Controllers;


[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private PersonRepository personRepository;

    public PersonController()
    {
        this.personRepository = new PersonRepository();
    }
    
    [HttpGet]
    public List<Person> GetPerson()
    {
        var person = new Person("Dawid", "Alibaba");
        return new List<Person> { person };
    }
    
    [HttpPost]
    public IActionResult CreatePerson(Person person)
    {
        try
        {
            this.personRepository.AddPerson(person);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpPut]
    public IEnumerable<Person> UpdatePerson()
    {
        return null;
    }
    
    [HttpDelete]
    public IEnumerable<Person> DeletePerson()
    {
        return null;
    }
}