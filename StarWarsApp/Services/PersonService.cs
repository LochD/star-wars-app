using StarWarsApp.Clients;
using StarWarsApp.Models;
using StarWarsApp.Repositories;

namespace StarWarsApp.Services;

public class PersonService
{
    private readonly StarWarsDbContext dbContext;
    private StarWarsClient starWarsClient;
    private PersonRepository personRepository;
    private StarshipService starShipService;

    public PersonService(StarWarsDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.starWarsClient = new StarWarsClient();
        this.personRepository = new PersonRepository(this.dbContext);
        this.starShipService = new StarshipService(this.dbContext);
    }

    public async Task<bool> IsPersonExistOnStarWars(string personName)
    {
        var starWarsersNames = await this.starWarsClient.GetStarWarsersName();

        foreach (var name in starWarsersNames)
        {
            var equals = name.Equals(personName);
            if (equals)
            {
                return true;
            }
        }

        return false;
    }
    
    public async Task CreatePerson(Person person)
    {
        if (await this.personRepository.IsPersonExist(person))
        {
            return;
        }

        if (await IsPersonExistOnStarWars(person.Name) && !await this.personRepository.IsPersonExist(person))
        {
            List<string> starships = await this.starWarsClient.GetStarWarserships(person.Name);

            var createdStarShips = new List<StarShip>();
            foreach (string starshipName in starships)
            {
                var starship = new StarShip
                {
                    Name = starshipName,
                };
                createdStarShips.Add(starship);
            }

            var newCreatedPerson = new Person
            {
                Name = person.Name,
                Surname = person.Surname,
                StarShips = createdStarShips
            };

            await this.personRepository.CreatePersonAsync(newCreatedPerson);
            return;
        }

        if (!await IsPersonExistOnStarWars(person.Name))
        {
            var newCreatedPerson = new Person
            {
                Name = person.Name,
                Surname = person.Surname,
                StarShips = person.StarShips
            };
            
            await this.personRepository.CreatePersonAsync(newCreatedPerson);
        }
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
        personBeforeUpdate.Surname = person.Surname;
        await this.dbContext.SaveChangesAsync();
        return GetPersonById(person.Id);
    }
    
    public Person GetPersonByName(string personName)
    {
        return this.dbContext.People.FirstOrDefault(person => person.Name == personName);
    }              
    
    private Person GetPersonById(int personId)
    {
        return this.dbContext.People.FirstOrDefault(person => person.Id == personId);
    }
}