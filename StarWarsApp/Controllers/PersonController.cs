using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Models;
using StarWarsApp.Repositories;
using StarWarsApp.Requests;
using StarWarsApp.Services;

namespace StarWarsApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService personService;

    public PersonController(StarWarsDbContext dbContext)
    {
        this.personService = new PersonService(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(PersonCreateRequest personCreateRequest)
    {
        var person = new Person
        {
            Name = personCreateRequest.Name,
            Surname = personCreateRequest.Surname,
            StarShips = new List<StarShip>()
        };

        List<StarShipCreateRequest> starShips = personCreateRequest.StarShips;
        
        foreach (var starShipCreateRequest in starShips)
        {
            person.StarShips.Add(new StarShip { Name = starShipCreateRequest.Name });
        }
        
        await this.personService.CreatePerson(person);
        return Ok(person);
    }

    [HttpGet]
    public ActionResult<Person> GetPerson(string personName)
    {
        var person = this.personService.GetPersonByName(personName);
        return Ok(person);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson(Person person)
    {
        var updatedPerson = await this.personService.UpdatePersonAsync(person);
        return Ok(updatedPerson);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> DeletePerson(int personId)
    {
        await this.personService.DeletePerson(personId);
        return Ok();
    }
}