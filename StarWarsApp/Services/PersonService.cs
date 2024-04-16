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
                .Select(starshipName => new Starship { Name = starshipName })
                .ToList();

            var personWithStarShipsFromStarWarsClient = new Person
            {
                Name = person.Name,
                Starships = mappedStarShipsList
            };

            return await this.personRepository.CreatePersonAsync(personWithStarShipsFromStarWarsClient);
        }

        if (!await this.starWarsClient.IsPersonExist(person.Name))
        {
            //TO DO: always while creating person starship is created and connected to that person
            var newPerson = new Person
            {
                Name = person.Name,
                Starships = person.Starships?
                    .Select(ss => new Starship { Name = ss.Name })
                    .ToList() ?? new List<Starship>()
            };
            
            return await this.personRepository.CreatePersonAsync(newPerson);
        }

        throw new PersonExistException($"Person with name {person.Name} currently exist.");
    }

    public async Task DeletePerson(int personId)
    {
        await this.personRepository.DeletePerson(personId);
    }
    
    public async Task<Person> UpdatePersonAsync(Person person)
    {
        var personBeforeUpdate = this.personRepository.GetPersonById(person.Id);
        personBeforeUpdate.Name = person.Name;
        await this.dbContext.SaveChangesAsync();
        return this.personRepository.GetPersonById(person.Id);
    }
    
    public Person GetPersonByName(string personName)
    {
        return this.personRepository.GetPersonByName(personName);
    }
}