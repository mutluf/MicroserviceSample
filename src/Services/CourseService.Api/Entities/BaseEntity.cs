using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CourseService.Api.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
