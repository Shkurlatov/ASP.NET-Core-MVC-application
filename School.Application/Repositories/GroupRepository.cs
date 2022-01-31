using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class GroupRepository : Repository<Group>
    {
        public GroupRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IReadOnlyList<Group>> GetAllAsync()
        {
            return await _dbContext.Set<Group>().Include(x => x.Course).Include(x => x.Students).AsNoTracking().ToListAsync();
        }

        public override async Task<Group> GetByIdAsync(int groupId)
        {
            return await _dbContext.Set<Group>().Include(x => x.Course).Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public override async Task<IReadOnlyList<Group>> GetBySearchAsync(string searchTerm)
        {
            var groups = (from entity in _dbContext.Groups
                          where (entity.Name + " " + entity.Course.Name).Contains(searchTerm)
                          select entity).Include(x => x.Course).Include(x => x.Students).AsNoTracking().ToListAsync();

            return await groups;
        }

        public override async Task<IReadOnlyList<Group>> GetByParentAsync(int parentId)
        {
            var groups = (from entity in _dbContext.Groups
                          where entity.CourseId == parentId
                          select entity).Include(x => x.Course).Include(x => x.Students).AsNoTracking().ToListAsync();

            return await groups;
        }
    }
}
