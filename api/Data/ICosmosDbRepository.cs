using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Entities;

namespace StarLog.Data
{
    public interface ICosmosDbRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetItemsAsync(string query);
        Task<TEntity> GetItemAsync(string id);
        Task AddItemAsync(TEntity item);
        Task UpdateItemAsync(string id, TEntity item);
        Task DeleteItemAsync(string id);
    }
}