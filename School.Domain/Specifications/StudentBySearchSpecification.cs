using School.Domain.Entities;

namespace School.Domain.Specifications
{
    public class StudentBySearchSpecification : BaseSpecification<Student>
    {
        public StudentBySearchSpecification(string searchTerm)
            : base(s => (s.FirstName + " " + s.LastName + " " + s.Group.Name).ToLower().Contains(searchTerm.ToLower()))
        {
            AddInclude(s => s.Group);
        }

        public StudentBySearchSpecification() : base(null)
        {
            AddInclude(s => s.Group);
        }
    }
}
