using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    internal class AmountMongo
    {
        public Guid Id { get; set; }
        public long TotalBaseExpences { get; set; }
        public long TotalAditionalExpences { get; set; }
        public long Savings { get; set; }
        public long TotalIncomes { get; set; }
    }
}
