using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.Cosmos;
using StarLog.Data;
using StarLog.Entities;
using StarLog.Models;

namespace StarLog.Services
{
    public class CelestialObjectService : ICelestialObjectService
    {
        private readonly ICosmosDbRepository _celestialObjectRepository;
        private readonly IMapper _mapper;

        public CelestialObjectService(
            ICosmosDbRepositoryFactory factory,
            IMapper mapper)
        {
            _celestialObjectRepository =
                factory.GetCosmosDbRepository(Constants.ContainerNames.CelestialObjects);
            _mapper = mapper;
        }

        public async Task<List<CelestialObjectModel>> GetCelestialObjectsBySearchTermAsync(string searchTerm)
        {
            var queryDef = new QueryDefinition(Constants.QueryStrings.GetCelestialObjectsBySearchTerm)
                .WithParameter("@term", searchTerm);

            var items = await _celestialObjectRepository.GetItemsAsync<CelestialObject>(queryDef);

            List<CelestialObjectModel> results
                = items.Select(item => _mapper.Map<CelestialObjectModel>(item)).ToList();

            return results;
        }
    }
}