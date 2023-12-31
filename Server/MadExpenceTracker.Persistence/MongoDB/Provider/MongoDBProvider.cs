﻿using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Provider
{
    public class MongoDBProvider : IMongoDBProvider
    {
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
    }
}
