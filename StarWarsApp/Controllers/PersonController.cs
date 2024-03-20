using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Dtos;
using StarWarsApp.Models;
using StarWarsApp.Repositories;

namespace StarWarsApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly StarWarsDbContext dbContext;
    private readonly PersonRepository personRepository;

    public PersonController()
    {
        this.dbContext = new StarWarsDbContext();
        this.personRepository = new PersonRepository(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonDto personDto)
    {
        var person = new Person
        {
            Name = personDto.Name,
            Surname = personDto.Surname
        };

        await this.personRepository.AddPersonAsync(person);
        return Ok(person);
    }

    [HttpGet]
    public ActionResult<Person> GetPerson(string personName)
    {
        var person = this.personRepository.GetPersonByName(personName);
        return Ok(person);
    }

    [HttpPut("{personId}")]
    public async Task<IActionResult> UpdatePerson(int personId, PersonDto updatedPersonDto)
    {
        var person = new Person
        {
            Name = updatedPersonDto.Name,
            Surname = updatedPersonDto.Surname
        };
        
        var updatedPerson = await this.personRepository.UpdatePersonAsync(personId, person);
        
        return Ok(updatedPerson);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePerson(int personId)
    {
        await this.personRepository.DeletePerson(personId);
        return Ok();
    }
}