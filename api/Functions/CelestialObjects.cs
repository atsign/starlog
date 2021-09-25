using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using StarLog.Models;
using System.Threading.Tasks;
using StarLog.Services;

namespace StarLog.Functions
{
    public class CelestialObjects
    {
        private readonly ICelestialObjectService _celestialObjectService;

        public CelestialObjects(
            ICelestialObjectService celestialObjectService)
        {
            _celestialObjectService = celestialObjectService;
        }

        [FunctionName("CelestialObjects")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {   
            string searchTerm = ((string)req.Query["search"])?.ToUpper();

            List<CelestialObjectModel> results
                = await _celestialObjectService.GetCelestialObjectsBySearchTermAsync(searchTerm);

            return new OkObjectResult(results);
        }
    }
}
