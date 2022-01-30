using School.Domain.Entities;

namespace School.Domain.Specifications
{
    public sealed class CourseBySearchSpecification : BaseSpecification<Course>
    {
        public CourseBySearchSpecification(string searchTerm)
            : base(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
        {
            AddInclude(c => c.Groups);
        }
    }
}
