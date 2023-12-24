using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IConfigurationService
    {
        Configuration GetConfiguration();
        Configuration SetConfiguration(Configuration configurationToSave);
        bool UpdateConfiguration(Configuration configurationToSave);
    }
}
