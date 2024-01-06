using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MongoDB.Driver;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver.Linq;

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
                ConfigurationMongo amountsOnDb = _configurationCollection.FindSync(filter).First();
                return ConfigurationMapper.MapToModel(amountsOnDb);
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
                    ConfigurationMongo newConfigurationMongoMongo = new ConfigurationMongo() { SavingsRate = 20 };
                    _configurationCollection.InsertOne(newConfigurationMongoMongo);
                }
                else
                {
                    var filter = Builders<ConfigurationMongo>.Filter.Eq(e => e.SavingsRate, configOnDb.SavingsRate);
                    var update = Builders<ConfigurationMongo>.Update.Set(e => e.SavingsRate, configurationToSave.SavingsRate);
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
                var filter = Builders<ConfigurationMongo>.Filter.Eq(e => e.SavingsRate, configurationToSave.SavingsRate);
                var update = Builders<ConfigurationMongo>.Update
                .Set(e => e.SavingsRate, configurationToSave.SavingsRate);
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
