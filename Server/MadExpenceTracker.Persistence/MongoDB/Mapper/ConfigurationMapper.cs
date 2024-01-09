using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Persistence.MongoDB.Model;


namespace MadExpenceTracker.Persistence.MongoDB.Mapper
{
    public static class ConfigurationMapper
    {
        public static Configuration MapToModel(ConfigurationMongo input)
        {
            return new Configuration()
            {
                SavingsRate = input.SavingsRate,
                BaseExpencesRate = input.BaseExpencesRate,
                AditionalExpencesRate = input.AditionalExpencesRate
            };
        }
    }
}
