using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Models;

namespace StarLog.Services
{
    public interface IObservationService
    {
        Task<List<ObservationModel>> GetObservationsForUserAsync(string userId);
        Task<string> InsertObservationForUserAsync(ObservationModel observation, string userId);
    }
}