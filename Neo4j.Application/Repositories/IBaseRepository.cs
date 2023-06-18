using System.Linq.Expressions;
using Neo4j.Domain.Relations;

namespace Neo4j.Application.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task<T?> GetAsync(T entity);

    Task<IEnumerable<(T entity, TRelation relation)>?> GetAllWithRelationAsync<TRelation>()
        where TRelation : BaseRelation;

    Task<IEnumerable<(T entity, TRelation relation, TOther other)>?> GetAllWithRelationAsync<TRelation, TOther>()
        where TRelation : BaseRelation where TOther : class;

    Task<IEnumerable<(T entity, TRelation relation)>?> GetAllWithReverseRelationAsync<TRelation>()
        where TRelation : BaseRelation;

    Task<IEnumerable<(T entity, TRelation relation, TOther other)>?> GetAllWithReverseRelationAsync<TRelation, TOther>()
        where TRelation : BaseRelation where TOther : class;
    
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);

    Task AddRelationAsync<TRelation, TOther>
        (Expression<Func<T, bool>> entity, Expression<Func<TOther, bool>> other, TRelation relation)
        where TOther : class where TRelation : class;

    Task AddWithRelationAsync<TRelation, TOther>
        (T entity, TOther other, TRelation relation)
        where TOther : class where TRelation : class;
    
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}