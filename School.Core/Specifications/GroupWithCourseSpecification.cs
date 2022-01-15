using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public class GroupWithCourseSpecification : BaseSpecification<Group>
    {
        public GroupWithCourseSpecification(string name)
            : base(g => g.Name.ToLower().Contains(name.ToLower()))
        {
            AddInclude(g => g.Course);
        }

        public GroupWithCourseSpecification() : base(null)
        {
            AddInclude(g => g.Course);
        }
    }
}
