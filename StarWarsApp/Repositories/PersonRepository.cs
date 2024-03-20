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