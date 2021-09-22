using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Entities;

namespace StarLog.Data
{
    public interface ICosmosDbRepository
    {
        Task<IEnumerable<TEntity>> GetItemsAsync<TEntity>(string queryString)
          where TEntity : Entity;
        Task<TEntity> GetItemAsync<TEntity>(string id)
          where TEntity : Entity;
        Task AddItemAsync<TEntity>(TEntity item)
          where TEntity : Entity;
        Task UpdateItemAsync<TEntity>(string id, TEntity item)
          where TEntity : Entity;
        Task DeleteItemAsync<TEntity>(TEntity item)
          where TEntity: Entity;
    }
}