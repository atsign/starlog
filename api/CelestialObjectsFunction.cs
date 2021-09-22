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
using System.Threading.Tasks;
using StarLog.Data;
using System.Linq;

namespace StarLog.Function
{
    public class CelestialObjectsFunction
    {
        private readonly ICosmosDbRepository _celestialObjectRepository;

        public CelestialObjectsFunction(ICosmosDbRepositoryFactory factory)
        {
            _celestialObjectRepository =
                factory.GetCosmosDbRepository(Constants.ContainerNames.CelestialObjects);
        }

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
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {   
            string searchTerm = ((string)req.Query["search"])?.ToUpper();

            var queryDef = new QueryDefinition(Constants.QueryStrings.GetCelestialObjectsBySearchTerm)
                .WithParameter("@term", searchTerm);

            var items = await _celestialObjectRepository.GetItemsAsync<CelestialObject>(queryDef);
            var mapper = _mapperConfig.CreateMapper(); // TODO: Pull the mapper stuff into its own service

            List<CelestialObjectModel> results
                = items.Select(item => mapper.Map<CelestialObjectModel>(item)).ToList();

            return new OkObjectResult(results);
        }
    }
}
