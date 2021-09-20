using StarLog.Entities;

namespace StarLog.Data
{
    public interface ICosmosDbRepositoryFactory
    {
        ICosmosDbRepository<TEntity> GetCosmosDbRepository<TEntity>(string containerName)
            where TEntity : Entity;
    }
}