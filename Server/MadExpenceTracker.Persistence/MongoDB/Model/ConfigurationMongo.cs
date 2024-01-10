using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MadExpenceTracker.Persistence.MongoDB.Model
{
    public class ConfigurationMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ObjectId { get; set; }
        public byte SavingsRate { get; set; }
        public byte BaseExpencesRate { get; set; }
        public byte AditionalExpencesRate { get; set; }
    }
}
