using School.Domain.Entities;

namespace School.Domain.Specifications
{
    public class GroupBySearchSpecification : BaseSpecification<Group>
    {
        public GroupBySearchSpecification(string searchTerm)
            : base(g => (g.Name + " " + g.Course.Name).ToLower().Contains(searchTerm.ToLower()))
        {
            AddInclude(g => g.Course);
        }

        public GroupBySearchSpecification() : base(null)
        {
            AddInclude(g => g.Course);
        }
    }
}
