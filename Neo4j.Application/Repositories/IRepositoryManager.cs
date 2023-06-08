namespace Neo4j.Application.Repositories;

public interface IRepositoryManager
{
    IPersonRepository PersonRepository { get; }
}