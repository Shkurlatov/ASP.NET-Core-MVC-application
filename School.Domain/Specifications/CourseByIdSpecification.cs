using School.Domain.Entities;

namespace School.Domain.Specifications
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