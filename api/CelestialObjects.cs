using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace StarLog
{
    public class CelestialObject
    {
        public string Name { get; set; }
        public string RightAscension { get; set; }
        public string Declination { get; set; }
        public int Id { get; set; }
    }

    public static class CelestialObjects
    {
        [FunctionName("CelestialObjects")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<CelestialObject> responseMessage = new List<CelestialObject> {
                new CelestialObject {
                    Name = "Coma Star Cluster",
                    RightAscension = "12hr 23m 34s",
                    Declination = "+25deg 45' 33\"",
                    Id = 2
                },
                new CelestialObject
                {
                    Name = "Messier 13",
                    RightAscension = "16hr 42m 28s",
                    Declination = "+36deg 25' 08\"",
                    Id = 1
                }
            };

            return new OkObjectResult(responseMessage);
        }
    }
}
