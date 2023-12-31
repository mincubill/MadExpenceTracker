using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class AmountsMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public Guid Id { get; set; }
        public IEnumerable<AmountMongo> Amount { get; set; }
    }
}
