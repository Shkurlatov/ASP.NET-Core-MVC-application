using School.Domain.Entities.Base;
using System.Collections.Generic;

namespace School.Domain.Entities
{
    public class Group : Entity
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public Course Course { get; set; }
        public ICollection<Student> Students { get; private set; }

        public static Group Create(int groupId, int courseId, string name)
        {
            var group = new Group
            {
                Id = groupId,
                CourseId = courseId,
                Name = name
            };
            return group;
        }
    }
}
