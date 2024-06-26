using Microsoft.EntityFrameworkCore;
using StarWarsApp.Exceptions;
using StarWarsApp.Models;
using StarWarsApp.Responses;

namespace StarWarsApp.Repositories
{
    public class PersonRepository : IDisposable, IAsyncDisposable
    {
        private readonly StarWarsDbContext dbContext;

        public PersonRepository(StarWarsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<PersonCreatedResponse> CreatePersonAsync(Person person)
        {
            await this.dbContext.People.AddAsync(person);
            await this.dbContext.SaveChangesAsync();
            return new PersonCreatedResponse(person);
        }
        public async Task DeletePerson(int personId)
        {
            var personToDelete = GetPersonById(personId);
            this.dbContext.People.Remove(personToDelete);
            await this.dbContext.SaveChangesAsync();
        }
        
        // private Person GetPersonById(int personId)
        // {
        //     // .Include
        //    return this.dbContext.People.FirstOrDefault(person => person.Id == personId);
        // }
        
        public async Task AssignStarShipToPerson(int personId, int starShipId)
        {
            var person = GetPersonById(personId);
            var starShip = await this.dbContext.StarShips.FindAsync(starShipId);
            person.Starships.Add(starShip);
            this.dbContext.People.Update(person);
            await this.dbContext.SaveChangesAsync();
        }
        
        public async Task<bool> IsPersonExist(Person person)
        {
            return await dbContext.People.AnyAsync(p => p.Name == person.Name);
        }
        
        public Person GetPersonByName(string personName)
        {
            return this.dbContext.People.FirstOrDefault(person => person.Name == personName) ?? 
                   throw new PersonNotFoundException($"Person with name {personName} not exist");
        }
    
        public Person GetPersonById(int personId)
        {
            return this.dbContext.People.FirstOrDefault(person => person.Id == personId) ?? 
                   throw new PersonNotFoundException($"Person with id {personId} not exist");
        }
        
        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await this.dbContext.DisposeAsync();
        }
    }
}