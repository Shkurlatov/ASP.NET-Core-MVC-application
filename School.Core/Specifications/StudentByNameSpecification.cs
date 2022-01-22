using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public class StudentByNameSpecification : BaseSpecification<Student>
    {
        public StudentByNameSpecification(string name)
            : base(s => (s.FirstName + " " + s.LastName + " " + s.Group.Name).ToLower().Contains(name.ToLower()))
        {
            AddInclude(s => s.Group);
        }

        public StudentByNameSpecification() : base(null)
        {
            AddInclude(s => s.Group);
        }
    }
}
