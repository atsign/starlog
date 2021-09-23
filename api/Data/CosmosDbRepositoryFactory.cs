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
        private readonly CosmosClient _client;

        public CosmosDbRepositoryFactory(
            IOptions<CosmosDbOptions> cosmosDbOptions,
            CosmosClient client)
        {
            _databaseName = cosmosDbOptions.Value?.DatabaseName
                ?? throw new ArgumentException(nameof(CosmosDbOptions.DatabaseName));
            
            _client = client;
        }

        public ICosmosDbRepository GetCosmosDbRepository(string containerName)
        {
            return new CosmosDbRepository(_client, _databaseName, containerName);
        }
    }
}