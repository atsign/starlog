using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarLog.Services;
using StarLog.Models;
using AutoMapper;
using StarLog.Extensions;

namespace StarLog.Functions
{
    public class InsertObservation
    {
        private readonly IObservationService _observationService;
        private IMapper _mapper;

        public InsertObservation(
            IObservationService observationService,
            IMapper mapper)
        {
            _observationService = observationService;
            _mapper = mapper;
        }

        [FunctionName("InsertObservation")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.ParseClaimsPrincipal().GetUserId();

            if (userId == null)
                return new UnauthorizedResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var observationModel = JsonConvert.DeserializeObject<ObservationModel>(requestBody);

            var itemId = await _observationService.InsertObservationForUserAsync(observationModel, userId);

            return new OkObjectResult(itemId);
        }
    }
}
