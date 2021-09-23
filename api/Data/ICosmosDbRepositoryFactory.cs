using StarLog.Entities;

namespace StarLog.Data
{
    public interface ICosmosDbRepositoryFactory
    {
        ICosmosDbRepository GetCosmosDbRepository(string containerName);
    }
}