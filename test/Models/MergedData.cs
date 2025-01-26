using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace test.Models
{
    public class MergedData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonDateTimeOptions]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ApiResponse Weekly { get; set; } = new();
        public ApiResponse Monthly { get; set; } = new();
    }
}
