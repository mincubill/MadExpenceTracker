using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;

namespace MadExpenceTracker.Persistence.Test.Fixture
{
    public static class ConfigurationFixture
    {
        public static Configuration GetConfiguration()
        {
            return new Configuration()
            {
                SavingsRate = 20,
                BaseExpencesRate = 50,
                AditionalExpencesRate = 30
            };
        }
        
        public static ConfigurationMongo GetConfigurationMongo()
        {
            return new ConfigurationMongo()
            {
                ObjectId = "",
                SavingsRate = 20,
                BaseExpencesRate = 50,
                AditionalExpencesRate = 30
            };
        }
    }
}
