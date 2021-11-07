using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StarLog.Extensions;
using StarLog.Services;
using AutoMapper;
using StarLog.Models;

namespace StarLog.Function
{
    public class UpdateObservation
    {
        private readonly IObservationService _observationService;
        private IMapper _mapper;

        public UpdateObservation(
            IObservationService observationService,
            IMapper mapper)
        {
            _observationService = observationService;
            _mapper = mapper;
        }

        [FunctionName("UpdateObservation")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string userId = req.ParseClaimsPrincipal().GetUserId();

            if (userId == null)
                return new UnauthorizedResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedObservation = JsonConvert.DeserializeObject<ObservationModel>(requestBody);

            if (await _observationService.UpdateObservationForUserAsync(updatedObservation, userId))
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
