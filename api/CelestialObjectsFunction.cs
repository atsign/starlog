using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using StarLog.Entities;
using System.Collections.Generic;
using StarLog.Models;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace StarLog.Function
{
    public static class CelestialObjectsFunction
    {
        private static string URL = Environment.GetEnvironmentVariable("AzureCosmosEndpoint");
        private static string KEY = Environment.GetEnvironmentVariable("AzureCosmosKey");
        private static MapperConfiguration _mapperConfig = new MapperConfiguration(cfg =>
            cfg.CreateMap<CelestialObject, CelestialObjectModel>()
                .ForMember(dest => dest.Declination, opts => opts.MapFrom(src => src.Dec))
                .ForMember(dest => dest.RightAscension, opts => opts.MapFrom(src => src.RA))
                .ForMember(dest => dest.CommonNames, opts =>
                    opts.MapFrom(src =>
                        src.CommonNames.Length > 0
                            ? src.CommonNames.Split(",", System.StringSplitOptions.None)
                            : null as string[]
                        )));

        [FunctionName("CelestialObjects")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {   
            string searchTerm = ((string)req.Query["search"])?.ToUpper();
            string queryString = "SELECT TOP 5 * FROM c WHERE CONTAINS(UPPER(c.Name), @term) OR CONTAINS(UPPER(c[\"Common names\"]), @term)";

            QueryDefinition queryDef = new QueryDefinition(queryString)
                .WithParameter("@term", searchTerm);

            CosmosClient cosmosClient = new CosmosClient(URL, KEY);

            var iterator = cosmosClient
                .GetDatabase("StarLog")
                .GetContainer("CelestialObjects")
                .GetItemQueryIterator<CelestialObject>(queryDef);

            List<CelestialObjectModel> results = new List<CelestialObjectModel>();
            while(iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync();
                var mapper = _mapperConfig.CreateMapper();
                foreach(var item in result.Resource)
                {
                    results.Add(mapper.Map<CelestialObjectModel>(item));
                }
            }

            return new OkObjectResult(results);
        }
    }
}
