using School.Domain.Entities;
using School.Domain.Specifications.Base;

namespace School.Domain.Specifications
{
    public sealed class CourseByNameSpecification : BaseSpecification<Course>
    {
        public CourseByNameSpecification(string name)
            : base(c => c.Name.ToLower().Contains(name.ToLower()))
        {
            AddInclude(c => c.Groups);
        }
    }
}
