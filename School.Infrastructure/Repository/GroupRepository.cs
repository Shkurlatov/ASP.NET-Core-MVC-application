using School.Domain.Entities;
using School.Domain.Repositories;
using School.Domain.Specifications;
using School.Infrastructure.Data;
using School.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Infrastructure.Repository
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            var spec = new GroupByIdSpecification(groupId);
            var group = (await GetAsync(spec)).FirstOrDefault();
            return group;
        }

        public async Task<IEnumerable<Group>> GetGroupListAsync()
        {
            var spec = new GroupByNameSpecification();
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Group>> GetGroupByNameAsync(string name)
        {
            var spec = new GroupByNameSpecification(name);
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Group>> GetGroupByCourseAsync(int courseId)
        {
            var spec = new GroupByCourseSpecification(courseId);
            return await GetAsync(spec);
        }
    }
}
