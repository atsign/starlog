using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace StarLog.Extensions
{
    public static class ServiceCollectionCosmosDbExtensions
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, Uri serviceEndpoint,
            string authKey, string databaseName, List<string> collectionNames)
        {
            return services;
        }
    }
}