using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Dtos;

namespace StarWarsApp.Controllers;


[ApiController]
[Route("[controller]")]
public class StarShipController : ControllerBase
{
    
    [HttpGet]
    public IEnumerable<StarShip> GetPerson()
    {
        var starShip = new StarShip("Luke star ship");
        return new List<StarShip> { starShip };
    }
    
    [HttpPost]
    public IEnumerable<StarShip> CreatePerson()
    {
        return null;
    }
    
    [HttpPut]
    public IEnumerable<StarShip> UpdatePerson()
    {
        return null;
    }
    
    [HttpDelete]
    public IEnumerable<StarShip> DeletePerson()
    {
        return null;
    }
}