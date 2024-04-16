using StarWarsApp.Clients;
using StarWarsApp.Exceptions;
using StarWarsApp.Models;
using StarWarsApp.Repositories;
using StarWarsApp.Responses;

namespace StarWarsApp.Services;

public class PersonService
{
    private readonly StarWarsDbContext dbContext;
    private StarWarsClient starWarsClient;
    private PersonRepository personRepository;

    public PersonService(StarWarsDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.starWarsClient = new StarWarsClient();
        this.personRepository = new PersonRepository(this.dbContext);
    }
    
    public async Task<PersonCreatedResponse> CreatePerson(Person person)
    {
        if (await this.starWarsClient.IsPersonExist(person.Name) && !await this.personRepository.IsPersonExist(person))
        {
            var starShipsFromStarWarsClient = await this.starWarsClient.GetStarWarserShipsByPersonName(person.Name);
            var mappedStarShipsList = starShipsFromStarWarsClient
                .Select(starshipName => new StarShip { Name = starshipName })
                .ToList();

            var personWithStarShipsFromStarWarsClient = new Person
            {
                Name = person.Name,
                StarShips = mappedStarShipsList
            };

            return await this.personRepository.CreatePersonAsync(personWithStarShipsFromStarWarsClient);
        }

        if (!await this.starWarsClient.IsPersonExist(person.Name))
        {
            var newCreatedPerson = new Person
            {
                Name = person.Name,
                StarShips = person.StarShips
            };
            
            return await this.personRepository.CreatePersonAsync(newCreatedPerson);
        }

        throw new PersonExistException($"Person with name {person.Name} currently exist.");
    }

    public async Task DeletePerson(int personId)
    {
        var personToDelete = GetPersonById(personId);
        this.dbContext.People.Remove(personToDelete);
        await this.dbContext.SaveChangesAsync();
    }
    
    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var personBeforeUpdate = GetPersonById(person.Id);
        personBeforeUpdate.Name = person.Name;
        await this.dbContext.SaveChangesAsync();
        return GetPersonById(person.Id);
    }
    
    public Person GetPersonByName(string personName)
    {
        return this.dbContext.People.FirstOrDefault(person => person.Name == personName) ?? 
               throw new PersonNotFoundException($"Person with name {personName} not exist");
    }              
    
    private Person GetPersonById(int personId)
    {
        return this.dbContext.People.FirstOrDefault(person => person.Id == personId) ?? 
               throw new PersonNotFoundException($"Person with id {personId} not exist");
    }
}