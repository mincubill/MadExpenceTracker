using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.MongoConfiguration
{
    public class Connection
    {
        private List<string> _collections = new List<string>() { "expence", "income", "amounts", "configuration", "monthIndex" };
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string DatabaseName { get; set; } = "MadExpencesTracker";

        public MongoClient GetClient()
        {
            return new MongoClient(ConnectionString);
        }

        public IMongoDatabase GetDatabase(MongoClient mongoClient)
        {
            return mongoClient.GetDatabase(DatabaseName);
        }

        public void CreateCollectionsIfNotExists(MongoClient mongoClient) 
        {
            IMongoDatabase database = GetDatabase(mongoClient);
            var collections = database.ListCollectionNames().ToList();
            foreach (string collection in _collections)
            {
                if(!collections.Contains(collection))
                {
                    database.CreateCollection(collection);
                }
            }
        }
    }
}
