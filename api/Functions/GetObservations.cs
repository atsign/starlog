using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StarLog.Services;
using StarLog.Extensions;

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
            [HttpTrigger(AuthorizationLevel.User, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.ParseClaimsPrincipal().GetUserId();

            if (userId == null)
                return new UnauthorizedResult();

            var observations = await _observationService.GetObservationsForUserAsync(userId);
            return new OkObjectResult(observations);
        }
    }
}
