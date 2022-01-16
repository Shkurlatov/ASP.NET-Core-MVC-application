using School.Core.Entities;
using School.Core.Repositories;
using School.Core.Specifications;
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

        public async Task<Group> GetGroupWithStudentsAsync(int groupId)
        {
            var spec = new GroupWithStudentsSpecification(groupId);
            var group = (await GetAsync(spec)).FirstOrDefault();
            return group;
        }

        public async Task<IEnumerable<Group>> GetGroupListAsync()
        {
            var spec = new GroupWithCourseSpecification();
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Group>> GetGroupByNameAsync(string name)
        {
            var spec = new GroupWithCourseSpecification(name);
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Group>> GetGroupByCourseAsync(int courseId)
        {
            return await _dbContext.Groups
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }
    }
}
