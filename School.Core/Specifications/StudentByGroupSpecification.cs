using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public sealed class StudentByGroupSpecification : BaseSpecification<Student>
    {
        public StudentByGroupSpecification(int groupId)
            : base(s => s.GroupId == groupId)
        {
            AddInclude(s => s.Group);
        }
    }
}
