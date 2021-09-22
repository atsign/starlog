using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using StarLog.Options;

namespace StarLog.Data
{
    public class CosmosDbRepositoryFactory : ICosmosDbRepositoryFactory
    {
        private readonly string _databaseName;
        private readonly List<string> _containerNames;
        private readonly CosmosClient _client;

        public CosmosDbRepositoryFactory(
            IOptions<CosmosDbOptions> cosmosDbOptions,
            CosmosClient client)
        {
            _databaseName = cosmosDbOptions.Value?.DatabaseName
                ?? throw new ArgumentException(nameof(CosmosDbOptions.DatabaseName));
            _containerNames = cosmosDbOptions.Value?.ContainerNames?.Select(c => c.Name)?.ToList()
                ?? throw new ArgumentException(nameof(CosmosDbOptions.ContainerNames));
            
            _client = client;
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