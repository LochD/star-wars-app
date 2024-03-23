using StarWarsApp.Models;

namespace StarWarsApp.Repositories
{
    public class PersonRepository : IDisposable, IAsyncDisposable
    {
        private readonly StarWarsDbContext dbContext;

        public PersonRepository(StarWarsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task AddPersonAsync(Person person)
        {
            await this.dbContext.People.AddAsync(person);
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
        
        public async Task DeletePerson(int personId)
        {
            var personToDelete = GetPersonById(personId);
            this.dbContext.People.Remove(personToDelete);
            await this.dbContext.SaveChangesAsync();
        }
        
        private Person GetPersonById(int personId)
        {
           return dbContext.People.FirstOrDefault(person => person.Id == personId);
        }
        
        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }
    }
}