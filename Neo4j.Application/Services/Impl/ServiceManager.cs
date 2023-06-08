using Neo4j.Application.Repositories;

namespace Neo4j.Application.Services.Impl;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IPersonService> _personService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _personService = new Lazy<IPersonService>(() => new PersonService(repositoryManager));
    }

    public IPersonService PersonService => _personService.Value;
}