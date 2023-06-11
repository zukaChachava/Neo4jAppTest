using Neo4j.Application.Repositories;
using Neo4j.Domain.Entities;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4j.Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(IBoltGraphClient client) : base(client)
    {
    }

    public override async Task<Person?> GetAsync(Person entity)
    {
        var result = await GetEntity(entity).ResultsAsync;
        return result.SingleOrDefault();
    }

    protected override ICypherFluentQuery<Person> GetEntity(Person entity)
    {
        return Client.Cypher.Match("(p:Person)")
            .Where((Person p) => p.Id == entity.Id)
            .Return(p => p.As<Person>());
    }
}