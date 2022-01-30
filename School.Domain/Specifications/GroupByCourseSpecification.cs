using School.Domain.Entities;

namespace School.Domain.Specifications
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
