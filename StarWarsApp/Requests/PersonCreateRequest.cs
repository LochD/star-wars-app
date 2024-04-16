namespace StarWarsApp.Requests;

public class PersonCreateRequest
{
    public string Name { get; set; }
    public List<StarShipCreateRequest> StarShips { get; set; }
}