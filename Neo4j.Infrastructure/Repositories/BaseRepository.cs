using Neo4j.Application.Repositories;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4j.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    protected BaseRepository(IBoltGraphClient client)
    {
        Client = client;
    }
    
    protected IBoltGraphClient Client { get; private set; }

    public abstract Task<T?> GetAsync(T entity);

    protected abstract ICypherFluentQuery<T> GetEntity(T entity);

    public virtual async Task<T> AddAsync(T entity)
    {
        await Client.Cypher.Create($"(e:{typeof(T).Name} $newEntity)").WithParam("newEntity", entity).ExecuteWithoutResultsAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        await GetEntity(entity).Set("e = $entity").WithParam("entity", entity).ExecuteWithoutResultsAsync();
        return entity;
    }

    public virtual Task DeleteAsync(T entity) =>
        GetEntity(entity).DetachDelete("").ExecuteWithoutResultsAsync();
}