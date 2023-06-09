using System.Linq.Expressions;
using Neo4j.Application.Repositories;
using Neo4j.Domain.Entities;
using Neo4j.Domain.Relations;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace Neo4j.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
{
    protected BaseRepository(IBoltGraphClient client)
    {
        Client = client;
    }

    protected IBoltGraphClient Client { get; private set; }

    public abstract Task<T?> GetAsync(T entity);

    public Task<IEnumerable<T>> GetAllAsync() =>
        Client.Cypher.Match($"(e:{typeof(T).Name})").Return(e => e.As<T>()).ResultsAsync;

    public async Task<IEnumerable<(T entity, TRelation relation)>?> GetAllWithRelationAsync<TRelation>()
        where TRelation : BaseRelation
    {
        var result = await Client.Cypher
            .Match($"(e:{typeof(T)})-[rel:{typeof(TRelation).Name}->()")
            .Return((from, rel) =>
                new
                {
                    Entity = from.As<T>(),
                    Relation = rel.As<TRelation>()
                }).ResultsAsync;

        return result?.Select(r => (r.Entity, r.Relation));
    }

    public async Task<IEnumerable<(T entity, TRelation relation, TOther other)>?> GetAllWithRelationAsync<TRelation,
        TOther>()
        where TRelation : BaseRelation where TOther : class
    {
        var result = await Client.Cypher
            .Match($"(e:{typeof(T)})-[rel:{typeof(TRelation).Name}->(o:{typeof(TOther).Name}")
            .Return((from, rel, other) =>
                new
                {
                    Entity = from.As<T>(),
                    Relation = rel.As<TRelation>(),
                    Other = other.As<TOther>()
                }).ResultsAsync;

        return result?.Select(r => (r.Entity, r.Relation, r.Other));
    }

    public async Task<IEnumerable<(T entity, TRelation relation)>?> GetAllWithReverseRelationAsync<TRelation>()
        where TRelation : BaseRelation
    {
        var result = await Client.Cypher
            .Match($"(e:{typeof(T)})<-[rel:{typeof(TRelation).Name}-()")
            .Return((from, rel) =>
                new
                {
                    Entity = from.As<T>(),
                    Relation = rel.As<TRelation>()
                }).ResultsAsync;

        return result?.Select(r => (r.Entity, r.Relation));
    }

    public async Task<IEnumerable<(T entity, TRelation relation, TOther other)>?> GetAllWithReverseRelationAsync<
        TRelation, TOther>()
        where TRelation : BaseRelation where TOther : class
    {
        var result = await Client.Cypher
            .Match($"(e:{typeof(T)})<-[rel:{typeof(TRelation).Name}-(o:{typeof(TOther).Name})")
            .Return((from, rel, other) =>
                new
                {
                    Entity = from.As<T>(),
                    Relation = rel.As<TRelation>(),
                    Other = other.As<TOther>()
                }).ResultsAsync;

        return result?.Select(r => (r.Entity, r.Relation, r.Other));
    }

    protected abstract ICypherFluentQuery<T> GetEntity(T entity);

    public virtual async Task<T> AddAsync(T entity)
    {
        await Client.Cypher.Create($"(e:{typeof(T).Name} $newEntity)").WithParam("newEntity", entity)
            .ExecuteWithoutResultsAsync();
        return entity;
    }

    public virtual Task AddRelationAsync<TRelation, TOther>
        (Expression<Func<T, bool>> entity, Expression<Func<TOther, bool>> other, TRelation relation)
        where TOther : class where TRelation : class
    {
        string? firstNode = entity.Parameters[0].Name;
        string? secondNode = other.Parameters[0].Name;
        return Client.Cypher
            .Match($"({firstNode}:{typeof(T).Name})")
            .Where(entity)
            .Match($"({secondNode}:{typeof(TOther).Name})")
            .Where(other)
            .Create($"({firstNode})-[rel:{typeof(TRelation).Name} $relation]->({secondNode})")
            .WithParam("relation", relation)
            .ExecuteWithoutResultsAsync();
    }

    public virtual Task AddWithRelationAsync<TRelation, TOther>
        (T entity, TOther other, TRelation relation) where TOther : class where TRelation : class
    {
        return Client.Cypher
            .Create($"(e:{typeof(T).Name} $entity)").WithParam("entity", entity)
            .Create($"(o:{typeof(TOther).Name}) $other").WithParam("other", other)
            .Create($"(e)-[rel:{typeof(TRelation).Name} $relation]->(o)").WithParam("relation", relation)
            .ExecuteWithoutResultsAsync();
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        await GetEntity(entity).Set("e = $entity").WithParam("entity", entity).ExecuteWithoutResultsAsync();
        return entity;
    }

    public virtual Task DeleteAsync(T entity) =>
        GetEntity(entity).DetachDelete("").ExecuteWithoutResultsAsync();
}