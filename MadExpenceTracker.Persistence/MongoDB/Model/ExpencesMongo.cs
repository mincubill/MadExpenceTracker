using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class ExpencesMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjectId { get; set; }
        public Guid Id { get; set; }
        public string RunningMonth { get; set; }
        public IEnumerable<ExpenceMongo> Expences { get; set; }
        public bool IsActive { get; set; }
    }
}
