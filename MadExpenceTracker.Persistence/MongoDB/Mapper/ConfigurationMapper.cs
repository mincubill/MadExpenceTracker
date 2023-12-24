using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;


namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    internal class ConfigurationMapper
    {
        public static Configuration MapToModel(ConfigurationMongo input)
        {
            return new Configuration()
            {
                SavingsRate = input.SavingsRate,
            };
        }

        public static ConfigurationMongo MapToMongo(Configuration input)
        {
            return new ConfigurationMongo()
            {
                SavingsRate = input.SavingsRate,
            };
        }
    }
}
