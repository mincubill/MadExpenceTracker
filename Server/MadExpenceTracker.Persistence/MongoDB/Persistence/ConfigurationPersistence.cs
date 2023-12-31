﻿using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MongoDB.Driver;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver.Linq;
using MadExpenceTracker.Core.Exceptions;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class ConfigurationPersistence : IConfigurationPersistence
    {
        private const string CollectionName = "configuration";
        private readonly IMongoCollection<ConfigurationMongo> _configurationCollection;

        public ConfigurationPersistence(IMongoDBProvider provider)
        {
            _configurationCollection = provider.GetCollection<ConfigurationMongo>(CollectionName);
        }
        
        public Configuration GetConfiguration()
        {
            try
            {
                var filter = Builders<ConfigurationMongo>.Filter.Empty;
                ConfigurationMongo condigurationOnDb = _configurationCollection.FindSync(filter).FirstOrDefault()
                    ?? throw new NoConfigurationException("No Configuration detected");
                return ConfigurationMapper.MapToModel(condigurationOnDb);
            }
            catch (TimeoutException)
            {
                throw;
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
                var filterEmpty = Builders<ConfigurationMongo>.Filter.Empty;
                var configOnDb = _configurationCollection.FindSync(filterEmpty).FirstOrDefault();
                if (configOnDb == null)
                {
                    ConfigurationMongo newConfigurationMongoMongo = new ConfigurationMongo() 
                    { 
                        SavingsRate = configurationToSave.SavingsRate,
                        BaseExpencesRate = configurationToSave.BaseExpencesRate,
                        AditionalExpencesRate = configurationToSave.AditionalExpencesRate
                    };
                    _configurationCollection.InsertOne(newConfigurationMongoMongo);
                }
                else
                {
                    var filter = Builders<ConfigurationMongo>.Filter.Eq(e => e.SavingsRate, configOnDb.SavingsRate);
                    var update = Builders<ConfigurationMongo>.Update
                        .Set(e => e.SavingsRate, configurationToSave.SavingsRate)
                        .Set(e => e.AditionalExpencesRate, configurationToSave.AditionalExpencesRate)
                        .Set(e => e.BaseExpencesRate, configurationToSave.BaseExpencesRate);
                    var result = _configurationCollection.UpdateOne(filter, update);
                    configOnDb = _configurationCollection.FindSync(filterEmpty).FirstOrDefault();
                    return result.IsAcknowledged ? ConfigurationMapper.MapToModel(configOnDb) : null;
                }
                configOnDb = _configurationCollection.FindSync(filterEmpty).FirstOrDefault();
                return ConfigurationMapper.MapToModel(configOnDb);
            }
            catch (TimeoutException)
            {
                throw;
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
                var filter = Builders<ConfigurationMongo>.Filter.Empty;
                var update = Builders<ConfigurationMongo>.Update
                .Set(e => e.SavingsRate, configurationToSave.SavingsRate)
                .Set(e => e.AditionalExpencesRate, configurationToSave.AditionalExpencesRate)
                .Set(e => e.BaseExpencesRate, configurationToSave.BaseExpencesRate);
                var result = _configurationCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
