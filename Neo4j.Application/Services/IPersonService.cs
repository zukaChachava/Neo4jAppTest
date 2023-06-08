using Neo4j.Domain.Entities;

namespace Neo4j.Application.Services;

public interface IPersonService
{
    Task<Person> AddPersonAsync(Person person);
}