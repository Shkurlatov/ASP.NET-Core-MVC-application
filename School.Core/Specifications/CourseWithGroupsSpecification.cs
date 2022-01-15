using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public sealed class CourseWithGroupsSpecification : BaseSpecification<Course>
    {
        public CourseWithGroupsSpecification(int courseId)
            : base(b => b.Id == courseId)
        {
            AddInclude(b => b.Groups);
        }
    }
}
