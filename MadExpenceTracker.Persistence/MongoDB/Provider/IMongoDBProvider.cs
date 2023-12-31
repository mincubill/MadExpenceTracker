using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Provider
{
    public interface IMongoDBProvider
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
        void CreateCollectionsIfNotExists();
    }
}
