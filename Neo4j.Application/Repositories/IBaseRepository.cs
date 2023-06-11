namespace Neo4j.Application.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task<T?> GetAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}