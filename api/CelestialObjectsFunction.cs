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
        private readonly IMapper _mapper;

        public CelestialObjectsFunction(
            ICosmosDbRepositoryFactory factory,
            IMapper mapper)
        {
            _celestialObjectRepository =
                factory.GetCosmosDbRepository(Constants.ContainerNames.CelestialObjects);
            _mapper = mapper;
        }

        [FunctionName("CelestialObjects")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {   
            string searchTerm = ((string)req.Query["search"])?.ToUpper();

            var queryDef = new QueryDefinition(Constants.QueryStrings.GetCelestialObjectsBySearchTerm)
                .WithParameter("@term", searchTerm);

            var items = await _celestialObjectRepository.GetItemsAsync<CelestialObject>(queryDef);

            List<CelestialObjectModel> results
                = items.Select(item => _mapper.Map<CelestialObjectModel>(item)).ToList();

            return new OkObjectResult(results);
        }
    }
}
