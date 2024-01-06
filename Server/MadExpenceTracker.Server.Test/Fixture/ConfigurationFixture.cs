using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Model;

namespace MadExpenceTracker.Server.Test.Fixture
{
    public static class ConfigurationFixture
    {
        public static Configuration GetConfiguration()
        {
            return new Configuration() { SavingsRate = 20 };
        }

        public static ConfigurationApi GetConfigurationApi()
        {
            return new ConfigurationApi { SavingsRate = 20 };
        }
    }
}
