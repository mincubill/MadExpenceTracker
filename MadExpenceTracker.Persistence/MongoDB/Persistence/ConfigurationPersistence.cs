﻿using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MongoDB.Driver;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MongoDB.Driver.Linq;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class ConfigurationPersistence : IConfigurationPersistence
    {

        private readonly IMongoDatabase _mongoDatabase;
        private string _collectionName = "configuration";
        private IMongoCollection<ConfigurationMongo> _configurationCollection;

        public ConfigurationPersistence(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _configurationCollection = _mongoDatabase.GetCollection<ConfigurationMongo>(_collectionName);
        }

        public Configuration GetConfiguration()
        {
            try
            {
                ConfigurationMongo amountsOnDb = _configurationCollection.Find(_ => true).First();
                return ConfigurationMapper.MapToModel(amountsOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Configuration SetConfiguration(Configuration configurationToSave)
        {
            try
            {
                var configOnDb = _configurationCollection.Find(_ => true).FirstOrDefault();
                if (configOnDb == null)
                {
                    ConfigurationMongo newConfigurationMongoMongo = new ConfigurationMongo() { SavingsRate = 20 };
                    _configurationCollection.InsertOne(newConfigurationMongoMongo);
                }
                else
                {
                    var filter = Builders<ConfigurationMongo>.Filter.Eq(e => e.SavingsRate, configOnDb.SavingsRate);
                    var update = Builders<ConfigurationMongo>.Update.Set(e => e.SavingsRate, configurationToSave.SavingsRate);
                    var result = _configurationCollection.UpdateOne(filter, update);
                    return result.IsAcknowledged ? ConfigurationMapper.MapToModel(configOnDb) : null;
                }
                configOnDb = _configurationCollection.Find(_ => true).FirstOrDefault();

                return  ConfigurationMapper.MapToModel(configOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateConfiguration(Configuration configurationToSave)
        {
            try
            {
                var filter = Builders<ConfigurationMongo>.Filter.Eq(e => e.SavingsRate, configurationToSave.SavingsRate);
                var update = Builders<ConfigurationMongo>.Update
                .Set(e => e.SavingsRate, configurationToSave.SavingsRate);
                var result = _configurationCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}