using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Dtos;
using StarWarsApp.Models;
using StarWarsApp.Repositories;

namespace StarWarsApp.Controllers;


[ApiController]
[Route("[controller]")]
public class StarshipController : ControllerBase
{
    private readonly StarWarsDbContext dbContext;
    private readonly StarshipRepository _starshipRepository;

    public StarshipController()
    {
        this.dbContext = new StarWarsDbContext();
        this._starshipRepository = new StarshipRepository(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStarship(StarShipDto starshipDto)
    {
        var starShip = new StarShip
        {
            Name = starshipDto.Name
        };
        
        await this._starshipRepository.AddStarship(starShip);
        return Ok(starShip);
    }

    [HttpGet]
    public ActionResult<Person> GetStarship(string starShipName)
    {
        var person = this._starshipRepository.GetStarshipByName(starShipName);
        return Ok(person);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStarship(StarShip starShip)
    {
        var updatedPerson = await this._starshipRepository.UpdateStarship(starShip);
        return Ok(updatedPerson);
    }
    
    [HttpDelete("{starshipId}")]
    public async Task<IActionResult> DeleteStarship(int starshipId)
    {
        await this._starshipRepository.DeleteStarship(starshipId);
        return Ok();
    }
}