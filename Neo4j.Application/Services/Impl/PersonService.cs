using Neo4j.Application.Repositories;
using Neo4j.Domain.Entities;

namespace Neo4j.Application.Services.Impl;

public class PersonService : IPersonService
{
    private readonly IRepositoryManager _repositoryManager;
    
    public PersonService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public Task<Person> AddPersonAsync(Person person)
    {
        return _repositoryManager.PersonRepository.AddAsync(person);
    }
}