using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Data;
using StarLog.Models;

namespace StarLog.Services
{
    public class ObservationService : IObservationService
    {
        public ObservationService()
        {

        }

        public Task<List<ObservationModel>> GetObservationsForUserAsync(string userId)
        {
            // TODO: Fetch real results from CosmosDB repository
            var fakeResult = new List<ObservationModel>
            {
                new ObservationModel
                {
                    Id = "8c50e82d-9d81-49a1-be2d-622198e2e729",
                    Date = new System.DateTime(2021, 9, 25, 22, 30, 0),
                    Notes = "This is a test observation",
                    CelestialObject = new CelestialObjectModel
                    {
                        Name = "IC0190",
                        CommonNames = null,
                        RightAscension = "02:02:07.28",
                        Declination = "+23:32:59.4",
                        Id = "5715b745-c3fe-4a5d-8e7b-c99b803b89a0"
                    }
                }
            };

            if (userId == "7573f1d69c40c45120a91cf675ec34d6")
            {
                return Task.FromResult(fakeResult);
            }
            else
            {
                return Task.FromResult(null as List<ObservationModel>);
            }
        }
    }
}