using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class GroupRepository : Repository<Group>//, IGroupRepository
    {
        public GroupRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        //public async Task<Group> GetGroupByIdAsync(int groupId)
        //{
        //    var spec = new GroupByIdSpecification(groupId);
        //    var group = (await GetAsync(spec)).FirstOrDefault();
        //    return group;
        //}

        //public async Task<IEnumerable<Group>> GetGroupListAsync()
        //{
        //    var spec = new GroupBySearchSpecification();
        //    return await GetAsync(spec);
        //}

        //public async Task<IEnumerable<Group>> GetGroupByNameAsync(string name)
        //{
        //    var spec = new GroupBySearchSpecification(name);
        //    return await GetAsync(spec);
        //}

        //public async Task<IEnumerable<Group>> GetGroupByCourseAsync(int courseId)
        //{
        //    var spec = new GroupByCourseSpecification(courseId);
        //    return await GetAsync(spec);
        //}
    }
}
