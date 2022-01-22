using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public sealed class GroupByCourseSpecification : BaseSpecification<Group>
    {
        public GroupByCourseSpecification(int courseId)
            : base(g => g.CourseId == courseId)
        {
            AddInclude(g => g.Course);
            AddInclude(g => g.Students);
        }
    }
}
