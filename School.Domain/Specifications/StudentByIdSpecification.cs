using School.Domain.Entities;

namespace School.Domain.Specifications
{
    public sealed class StudentByIdSpecification : BaseSpecification<Student>
    {
        public StudentByIdSpecification(int studentId)
            : base(s => s.Id == studentId)
        {
            AddInclude(s => s.Group);
        }
    }
}
