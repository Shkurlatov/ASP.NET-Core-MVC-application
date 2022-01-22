using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
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
