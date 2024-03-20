using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Dtos;
using StarWarsApp.Models;
using StarWarsApp.Repositories;

namespace StarWarsApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    // private readonly StarWarsDbContext _context;
    //
    // public PersonController(StarWarsDbContext context)
    // {
    //     _context = context;
    // }
    // private PersonRepository personRepository;

    // public PersonController()
    // {
        // var dbContext = new StarWarsDbContext();
        // this.personRepository = new PersonRepository(dbContext);
    // }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonDto personDto)
    {
        var person = new Person
        {
            Name = personDto.Name,
            Surname = personDto.Surname
        };
        var dbContext = new StarWarsDbContext();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.People.AddAsync(person);
        await dbContext.SaveChangesAsync();
        // this.personRepository = new PersonRepository(dbContext);
        // await this.personRepository.AddPersonAsync(person);
        return Ok();
    }

    [HttpGet]
    public List<Person> GetPerson()
    {
        var person = new Person("Dawid", "Alibaba");
        return new List<Person> { person };
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