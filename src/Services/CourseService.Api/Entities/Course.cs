using MongoDB.Bson.Serialization.Attributes;

namespace CourseService.Api.Entities
{
    public class Course : BaseEntity
    {

        [BsonElement("Name")]
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string TeacherFullname { get; set; }
    }
}
