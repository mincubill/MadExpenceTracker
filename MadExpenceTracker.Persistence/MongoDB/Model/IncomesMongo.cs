using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class IncomesMongo
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public Guid Id { get; set; }
        public string RunningMonth { get; set; }
        public IEnumerable<IncomeMongo> Incomes { get; set; } = new List<IncomeMongo>();
        public bool IsActive { get; set; }
    }
}
