using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IConfigurationPersistence
    {
        Configuration GetConfiguration();
        Configuration SetConfiguration(Configuration configurationToSave);
        bool UpdateConfiguration(Configuration configurationToSave);
    }
}
