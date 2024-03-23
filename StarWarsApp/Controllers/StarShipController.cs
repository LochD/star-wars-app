using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Dtos;
using StarWarsApp.Models;
using StarWarsApp.Repositories;

namespace StarWarsApp.Controllers;


[ApiController]
[Route("[controller]")]
public class StarShipController : ControllerBase
{
    private readonly StarWarsDbContext dbContext;
    private readonly StarShipRepository starShipRepository;

    public StarShipController()
    {
        this.dbContext = new StarWarsDbContext();
        this.starShipRepository = new StarShipRepository(dbContext);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStarShip(StarShipDto starShipDto)
    {
        var starShip = new StarShip
        {
            Name = starShipDto.Name
        };
        
        await this.starShipRepository.AddStarShip(starShip);
        return Ok(starShip);
    }

    [HttpGet]
    public ActionResult<Person> GetStarShip(string starShipName)
    {
        var person = this.starShipRepository.GetStarShipByName(starShipName);
        return Ok(person);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStarShip(StarShip starShip)
    {
        var updatedPerson = await this.starShipRepository.UpdateStarShip(starShip);
        return Ok(updatedPerson);
    }
    
    [HttpDelete("{starShipId}")]
    public async Task<IActionResult> DeleteStarShip(int starShipId)
    {
        await this.starShipRepository.DeleteStarShip(starShipId);
        return Ok();
    }
}