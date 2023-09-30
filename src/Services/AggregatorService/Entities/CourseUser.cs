using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AggregatorService.Entities
{
    public class CourseUser
    {
        public string UserId { get; set; }

        public string CourseId { get; set; }
    }
}
