using MadExpenceTracker.Core.Interfaces.Utils;
using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Util
{
    public class DbInitialization : IDbInitialization
    {
        private readonly List<string> _collections = new List<string>() { "expence", "income", "amounts", "configuration", "monthIndex" };
        private readonly IMongoDatabase _database;

        public DbInitialization(string mongoUrl, string databaseName)
        {
            MongoUrl url = new MongoUrl($"{mongoUrl}/{databaseName}");
            MongoClientSettings settings = MongoClientSettings.FromUrl(url);
            MongoClient client = new MongoClient(settings);
            _database = client.GetDatabase(url.DatabaseName);
        }
        public void InitializeDb()
        {
            CreateCollectionsIfNotExists();
        }

        private void CreateCollectionsIfNotExists()
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
