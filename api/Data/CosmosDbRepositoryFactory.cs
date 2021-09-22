using System;
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

        public ICosmosDbRepository GetCosmosDbRepository(string containerName)
        {
            if (! _containerNames.Contains(containerName))
            {
                throw new ArgumentException($"Container {containerName} not found in container name collections. Is it missing from configuration?");
            }

            return new CosmosDbRepository(_client, _databaseName, containerName);
        }
    }
}