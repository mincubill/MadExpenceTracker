﻿using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;

namespace MadExpenceTracker.Core.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationPersistence _persistence;

        public ConfigurationService(IConfigurationPersistence persistence)
        {
            _persistence = persistence;
        }

        public Configuration GetConfiguration()
        {
            return _persistence.GetConfiguration();
        }

        public Configuration SetConfiguration(Configuration configurationToSave)
        {
            return _persistence.SetConfiguration(configurationToSave);
        }

        public bool UpdateConfiguration(Configuration configurationToSave)
        {
            return _persistence.UpdateConfiguration(configurationToSave);
        }
    }
}
