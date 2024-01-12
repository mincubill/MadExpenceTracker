using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IMonthIndexingService
    {
        MonthIndexes GetMonthsIndexes();
        MonthIndex GetMonthsIndex(Guid id);
        MonthIndexes AddMonthIndex(MonthIndex index);
    }
}
