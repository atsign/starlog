using System.Collections.Generic;
using System.Threading.Tasks;
using StarLog.Models;

namespace StarLog.Services
{
    public interface ICelestialObjectService
    {
         Task<List<CelestialObjectModel>> GetCelestialObjectsBySearchTermAsync(string searchTerm);
    }
}