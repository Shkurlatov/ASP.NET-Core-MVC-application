using School.Core.Entities;
using School.Core.Specifications.Base;

namespace School.Core.Specifications
{
    public class GroupByNameSpecification : BaseSpecification<Group>
    {
        public GroupByNameSpecification(string name)
            : base(g => (g.Name + " " + g.Course.Name).ToLower().Contains(name.ToLower()))
        {
            AddInclude(g => g.Course);
        }

        public GroupByNameSpecification() : base(null)
        {
            AddInclude(g => g.Course);
        }
    }
}
