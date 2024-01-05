using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
