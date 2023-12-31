using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class MonthIndexMongo
    {
        public Guid Id { get; set; }
        public string Month { get; set; }
        public Guid ExpencesId { get; set; }
        public Guid IncomesId { get; set; }
        public Guid AmountsId { get; set; }
        public byte SavingsRate { get; set; }
    }
}
