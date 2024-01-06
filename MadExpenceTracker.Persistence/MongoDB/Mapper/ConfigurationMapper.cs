using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;


namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    public class ConfigurationMapper
    {
        public static Configuration MapToModel(ConfigurationMongo input)
        {
            return new Configuration()
            {
                SavingsRate = input.SavingsRate,
            };
        }
    }
}
