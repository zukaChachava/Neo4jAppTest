using Neo4j.Application.Repositories;
using Neo4j.Driver;

namespace Neo4j.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IPersonRepository> _personRepository;

    public RepositoryManager(IDriver driver)
    {
        _personRepository = new Lazy<IPersonRepository>(() => new PersonRepository(driver));
    }

    public IPersonRepository PersonRepository => _personRepository.Value;
}