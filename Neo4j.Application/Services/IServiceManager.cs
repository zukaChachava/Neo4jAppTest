namespace Neo4j.Application.Services;

public interface IServiceManager
{
    IPersonService PersonService { get; }
}