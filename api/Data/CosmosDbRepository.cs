using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Entities;

namespace StarLog.Data
{
    public class CosmosDbRepository<TEntity> : ICosmosDbRepository<TEntity>
        where TEntity : Entity
    {
        public Task AddItemAsync(TEntity item)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetItemAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetItemsAsync(string query)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateItemAsync(string id, TEntity item)
        {
            throw new System.NotImplementedException();
        }
    }
}