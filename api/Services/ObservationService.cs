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
    public class ObservationService : IObservationService
    {
        private readonly ICosmosDbRepository _repository;
        private readonly IMapper _mapper;

        public ObservationService(
            ICosmosDbRepositoryFactory factory,
            IMapper mapper)
        {
            _repository = factory.GetCosmosDbRepository(Constants.ContainerNames.Observations);
            _mapper = mapper;
        }

        public async Task<List<ObservationModel>> GetObservationsForUserAsync(string userId)
        {
            var query = new QueryDefinition(Constants.QueryStrings.GetObservationsByUserId)
                .WithParameter("@userId", userId);
            
            var results = await _repository.GetItemsAsync<Observation>(query);

            return _mapper.Map<List<ObservationModel>>(results.ToList());
        }

        public async Task<string> InsertObservationForUserAsync(ObservationModel observationModel, string userId)
        {
            var observation = _mapper.Map<Observation>(observationModel);
            observation.UserId = userId;
            var itemId = await _repository.AddItemAsync(_mapper.Map<Observation>(observation));
            return itemId;
        }
    }
}