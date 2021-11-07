using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Models;

namespace StarLog.Services
{
    public interface IObservationService
    {
        Task<List<ObservationModel>> GetObservationsForUserAsync(string userId);
        Task<ObservationModel> GetObservationForUserByIdAsync(string userId, string observationId);
        Task<string> InsertObservationForUserAsync(ObservationModel observation, string userId);
        Task<bool> DeleteObservationForUserAsync(string observationId, string userId);
        Task<bool> UpdateObservationForUserAsync(ObservationModel observation, string userId);
    }
}