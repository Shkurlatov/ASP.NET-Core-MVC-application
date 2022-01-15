using School.Core.Entities.Base;
using System.Collections.Generic;

namespace School.Core.Entities
{
    public class Course : Entity
    {
        public Course()
        {
            Groups = new HashSet<Group>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Group> Groups { get; private set; }

        public static Course Create(int courseId, string name, string description)
        {
            var course = new Course
            {
                Id = courseId,
                Name = name,
                Description = description
            };
            return course;
        }
    }
}
