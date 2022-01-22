using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public sealed class GroupByIdSpecification : BaseSpecification<Group>
    {
        public GroupByIdSpecification(int groupId)
            : base(g => g.Id == groupId)
        {
            AddInclude(g => g.Course);
            AddInclude(g => g.Students);
        }
    }
}
