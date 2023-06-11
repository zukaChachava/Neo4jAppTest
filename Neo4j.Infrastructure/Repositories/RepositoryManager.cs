using Neo4j.Application.Repositories;
using Neo4jClient;

namespace Neo4j.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IPersonRepository> _personRepository;

    public RepositoryManager(IBoltGraphClient client)
    {
        _personRepository = new Lazy<IPersonRepository>(() => new PersonRepository(client));
    }

    public IPersonRepository PersonRepository => _personRepository.Value;
}