using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using StarLog.Entities;

namespace StarLog.Data
{
    public class CosmosDbRepository : ICosmosDbRepository
    {
        private Container _container;

        public CosmosDbRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<string> AddItemAsync<TEntity>(TEntity item) where TEntity : Entity
        {
            item.Id = Guid.NewGuid().ToString();
            var result = await _container.CreateItemAsync(item);
            return result.Resource.Id;
        }

        public async Task DeleteItemAsync<TEntity>(TEntity item) where TEntity : Entity
        {
            await _container.DeleteItemAsync<TEntity>(item.Id, new PartitionKey(item.Id));
        }

        public async Task<TEntity> GetItemAsync<TEntity>(string id) where TEntity : Entity
        {
            try
            {
                ItemResponse<TEntity> response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch(CosmosException cex) when (cex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> GetItemsAsync<TEntity>(string queryString) where TEntity : Entity
        {
            var queryDefinition = new QueryDefinition(queryString);

            return await GetItemsAsync<TEntity>(queryDefinition);
        }

        public async Task<IEnumerable<TEntity>> GetItemsAsync<TEntity>(QueryDefinition queryDefinition) where TEntity : Entity
        {
            var query = _container.GetItemQueryIterator<TEntity>(queryDefinition);
            List<TEntity> results = new List<TEntity>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync<TEntity>(string id, TEntity item) where TEntity : Entity
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}