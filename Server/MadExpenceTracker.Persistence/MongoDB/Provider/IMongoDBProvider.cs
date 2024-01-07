using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Provider
{
    public interface IMongoDBProvider
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
