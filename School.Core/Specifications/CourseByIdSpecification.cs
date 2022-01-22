using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public sealed class CourseByIdSpecification : BaseSpecification<Course>
    {
        public CourseByIdSpecification(int courseId)
            : base(c => c.Id == courseId)
        {
            AddInclude(c => c.Groups);
        }
    }
}