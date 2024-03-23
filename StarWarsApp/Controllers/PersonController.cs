using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Clients;
using StarWarsApp.Dtos;
using StarWarsApp.Models;
using StarWarsApp.Repositories;
using StarWarsApp.Services;

namespace StarWarsApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly StarWarsDbContext dbContext;
    private readonly PersonRepository personRepository;
    private readonly StarshipService _starshipService;
    private readonly PersonService personService;

    public PersonController()
    {
        this.dbContext = new StarWarsDbContext();
        this.personRepository = new PersonRepository(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonDto personDto)
    {
        if (await personService.IsPersonExistOnStarWars(personDto.Name))
        {
            var starShips = await _starshipService.GetPersonStarships(personDto.Name);
            var personFromStarWars = new Person
            {
                Name = personDto.Name,
                Surname = personDto.Surname,
                Starships =  starShips
            };
            
            await this.personRepository.AddPersonAsync(personFromStarWars);
            return Ok(personFromStarWars);
        }

        var person = new Person
        {
            Name = personDto.Name,
            Surname = personDto.Surname,
            Starships = personDto.Starships
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

    [HttpPut]
    public async Task<IActionResult> UpdatePerson(int personId, Person person)
    {
        var updatedPerson = await this.personRepository.UpdatePersonAsync(person);
        return Ok(updatedPerson);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePerson(int personId)
    {
        await this.personRepository.DeletePerson(personId);
        return Ok();
    }
}