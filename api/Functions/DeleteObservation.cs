using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StarLog.Services;
using AutoMapper;
using StarLog.Extensions;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace StarLog.Functions
{
    public class DeleteObservation
    {
        private readonly IObservationService _observationService;

        public DeleteObservation(
            IObservationService observationService,
            IMapper mapper)
        {
            _observationService = observationService;
        }

        [FunctionName("DeleteObservation")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.ParseClaimsPrincipal().GetUserId();
            bool deleted = false;

            if (userId == null)
                return new UnauthorizedResult();

            if (req.Query.TryGetValue("id", out StringValues ids))
            {
                var observationId = ids.First();
                deleted = await _observationService.DeleteObservationForUserAsync(observationId, userId);
            }

            if (deleted)
            {
                return new OkResult();
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}
