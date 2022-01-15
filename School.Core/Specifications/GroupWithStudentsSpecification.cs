using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public sealed class GroupWithStudentsSpecification : BaseSpecification<Group>
    {
        public GroupWithStudentsSpecification(int groupId)
            : base(b => b.Id == groupId)
        {
            AddInclude(b => b.Students);
        }
    }
}
