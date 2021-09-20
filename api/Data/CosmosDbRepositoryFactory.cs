using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using StarLog.Entities;

namespace StarLog.Data
{
    public class CosmosDbRepositoryFactory : ICosmosDbRepositoryFactory
    {
        private readonly string _databaseName;
        private readonly List<string> _containerNames;
        private readonly CosmosClient _client;

        public CosmosDbRepositoryFactory(
            string databaseName, List<string> containerNames, CosmosClient client)
        {
            _databaseName = databaseName;
            _containerNames = containerNames;
            _client = client;
        }

        ~CosmosDbRepositoryFactory()
        {
            _client.Dispose();
        }

        public ICosmosDbRepository<TEntity> GetCosmosDbRepository<TEntity>(string containerName)
            where TEntity : Entity
        {
            throw new System.NotImplementedException();
        }
    }
}