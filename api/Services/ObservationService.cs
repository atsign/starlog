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

        public async Task<bool> DeleteObservationForUserAsync(string observationId, string userId)
        {
            bool deleted = false;
            var observation = await GetObservationForUserByIdAsync(userId, observationId);

            if (observation != null)
            {
                await _repository.DeleteItemAsync(_mapper.Map<Observation>(observation));
                deleted = true;
            }

            return deleted;
        }

        public async Task<ObservationModel> GetObservationForUserByIdAsync(string userId, string observationId)
        {
            var query = new QueryDefinition(Constants.QueryStrings.GetObservationByItemIdAndUserId)
                .WithParameter("@userId", userId)
                .WithParameter("@itemId", observationId);
            
            var result = (await _repository.GetItemsAsync<Observation>(query))
                ?.ToList()
                ?.FirstOrDefault();

            return result != null
                ? _mapper.Map<ObservationModel>(result)
                : null;
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

        public async Task<bool> UpdateObservationForUserAsync(ObservationModel observationModel, string userId)
        {
            bool updated = false;
            var existingObservation = GetObservationForUserByIdAsync(userId, observationModel.Id);

            if (existingObservation != null)
            {
                var updatedObservation = _mapper.Map<Observation>(observationModel);
                updatedObservation.UserId = userId;
                await _repository.UpdateItemAsync(updatedObservation.Id, updatedObservation);
                updated = true;
            }

            return updated;
        }
    }
}