using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Provider
{
    public class MongoDBProvider : IMongoDBProvider
    {
        private readonly List<string> _collections = new List<string>() { "expence", "income", "amounts", "configuration", "monthIndex" };
        private readonly IMongoDatabase _database;

        public MongoDBProvider(string mongoUrl, string databaseName)
        {
            MongoUrl url = new MongoUrl($"{mongoUrl}/{databaseName}");
            MongoClientSettings settings = MongoClientSettings.FromUrl(url);
            MongoClient client = new MongoClient(settings);
            _database = client.GetDatabase(url.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public void CreateCollectionsIfNotExists()
        {
            var collections = _database.ListCollectionNames().ToList();
            foreach (string collection in _collections)
            {
                if (!collections.Contains(collection))
                {
                    _database.CreateCollection(collection);
                }
            }
        }
    }
}
