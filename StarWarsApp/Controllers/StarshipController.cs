using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Models;
using StarWarsApp.Repositories;
using StarWarsApp.Requests;

namespace StarWarsApp.Controllers;


[ApiController]
[Route("[controller]")]
public class StarshipController : ControllerBase
{
    private readonly StarshipRepository starshipRepository;

    public StarshipController(StarWarsDbContext dbContext)
    {
        this.starshipRepository = new StarshipRepository(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStarship(StarShipCreateRequest starShipCreateRequest)
    {
        var starShip = new StarShip
        {
            Name = starShipCreateRequest.Name
        };
        
        await this.starshipRepository.CreateStarShip(starShip);
        return Ok(starShip);
    }

    [HttpGet]
    public ActionResult<Person> GetStarship(string starShipName)
    {
        var person = this.starshipRepository.GetStarshipByName(starShipName);
        return Ok(person);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStarship(StarShip starShip)
    {
        var updatedPerson = await this.starshipRepository.UpdateStarship(starShip);
        return Ok(updatedPerson);
    }
    
    [HttpDelete("{starshipId}")]
    public async Task<IActionResult> DeleteStarship(int starshipId)
    {
        await this.starshipRepository.DeleteStarship(starshipId);
        return Ok();
    }
}