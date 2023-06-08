using System.Reflection;
using System.Security.Claims;
using System.Text;
using Neo4j.Application.Repositories;
using Neo4j.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Neo4j.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    protected readonly IDriver _driver;

    protected BaseRepository(IDriver driver)
    {
        _driver = driver;
    }

    public abstract Task<T?> GetAsync(T entity);

    public async Task<T> AddAsync(T entity)
    {
       await using IAsyncSession session =  _driver.AsyncSession();
       T loadedData = new T();
       await session.ExecuteWriteAsync(async tx =>
       {
           var result = await tx.RunAsync(
               $"""
                        CREATE (e:{typeof(T).Name})
                        SET {GenerateProperties(entity)}
                        RETURN e;
                    """, entity
               );
           
           if(!await result.FetchAsync())
               return;

           LoadProperties(loadedData, result.Current.Values);
       });
       return loadedData;
    }

    public Task<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    protected void LoadProperties(T entity, IReadOnlyDictionary<string, Object> properties)
    {
        foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
        {
            if (!properties.ContainsKey(propertyInfo.Name))
                continue;
            
            propertyInfo.SetValue(entity, properties[propertyInfo.Name]);
        }
    }

    protected string GenerateProperties(T entity)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
        {
            stringBuilder
                .Append('e')
                .Append('.')
                .Append(propertyInfo.Name)
                .Append('=')
                .Append('$')
                .Append(propertyInfo.Name)
                .Append(',');
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);

        return stringBuilder.ToString();
    }
}