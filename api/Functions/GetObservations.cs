using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StarLog.Services;

namespace StarLog.Functions
{
    public class GetObservations
    {
        private readonly IObservationService _observationService;

        public GetObservations(IObservationService observationService)
        {
            _observationService = observationService;
        }

        [FunctionName("Observations")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            // TODO: Get the user ID from Azure
            var observations = await _observationService.GetObservationsForUserAsync("7573f1d69c40c45120a91cf675ec34d6");
            return new OkObjectResult(observations);
        }
    }
}
