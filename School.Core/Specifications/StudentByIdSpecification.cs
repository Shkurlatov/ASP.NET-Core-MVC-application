using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
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
