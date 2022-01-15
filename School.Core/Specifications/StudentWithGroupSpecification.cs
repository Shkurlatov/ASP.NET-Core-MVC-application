using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public class StudentWithGroupSpecification : BaseSpecification<Student>
    {
        public StudentWithGroupSpecification(string lastName)
            : base(s => s.LastName.ToLower().Contains(lastName.ToLower()))
        {
            AddInclude(s => s.Group);
        }

        public StudentWithGroupSpecification() : base(null)
        {
            AddInclude(s => s.Group);
        }
    }
}
