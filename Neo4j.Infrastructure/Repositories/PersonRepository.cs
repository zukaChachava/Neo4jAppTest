using Neo4j.Application.Repositories;
using Neo4j.Domain.Entities;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace Neo4j.Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(IDriver driver) : base(driver)
    {
    }

    public override async Task<Person?> GetAsync(Person entity)
    {
        await using IAsyncSession session = _driver.AsyncSession();
        Person person = new Person();
        await session.ExecuteReadAsync( async tx =>
        {
            var result = await tx.RunAsync(
                $"""
                       MATCH (e:{nameof(Person)}
                       WHERE e.Id = $Id
                       RETURN e;
                    """, new {Id = person.Id}
                );
            
            LoadProperties(person, result.Current.Values);
        });

        return person;
    }
}