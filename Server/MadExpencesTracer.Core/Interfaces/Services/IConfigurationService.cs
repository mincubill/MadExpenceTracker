using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        Configuration GetConfiguration();
        Configuration SetConfiguration(Configuration configurationToSave);
        Configuration UpdateConfiguration(Configuration configurationToSave);
    }
}
