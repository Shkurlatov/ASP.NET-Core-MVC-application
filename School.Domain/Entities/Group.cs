using System.Collections.Generic;

namespace School.Domain.Entities
{
    public class Group : Entity
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public Course Course { get; set; }
        public ICollection<Student> Students { get; private set; }
    }
}
