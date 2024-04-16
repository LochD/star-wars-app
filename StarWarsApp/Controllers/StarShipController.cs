using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Models;
using StarWarsApp.Repositories;
using StarWarsApp.Requests;
using StarWarsApp.Services;

namespace StarWarsApp.Controllers;


[ApiController]
[Route("[controller]")]
public class StarShipController : ControllerBase
{
    private readonly StarshipService starshipService;

    public StarShipController(StarWarsDbContext dbContext)
    {
        this.starshipService = new StarshipService(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStarShip(StarShipCreateRequest starShipCreateRequest)
    {
        var starShip = new Starship
        {
            Name = starShipCreateRequest.Name
        };
        
        await this.starshipService.CreateStarship(starShip);
        return Ok(starShip);
    }

    [HttpGet]
    public ActionResult<Person> GetStarship(string starShipName)
    {
        var person = this.starshipService.GetStarshipByName(starShipName);
        return Ok(person);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStarship(Starship starship)
    {
        var updatedPerson = await this.starshipService.UpdateStarship(starship);
        return Ok(updatedPerson);
    }
    
    [HttpDelete("{starshipId}")]
    public async Task<IActionResult> DeleteStarship(int starshipId)
    {
        await this.starshipService.DeleteStarship(starshipId);
        return Ok();
    }
}