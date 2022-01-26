using School.Domain.Entities;
using School.Domain.Specifications.Base;

namespace School.Domain.Specifications
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
