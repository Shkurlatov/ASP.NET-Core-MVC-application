using System.Collections.Generic;

namespace School.Domain.Entities
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Group> Groups { get; private set; }
    }
}
