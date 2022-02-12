using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities.Studies;
using School.Domain.Entities.Users;
using School.Domain.Interfaces.Studies;
using School.Domain.Interfaces;
using School.Persistence.Data;

namespace School.Application.Repositories.Studies
{
    public class GroupRepository : IStudyRepository<Group>
    {
        private readonly SchoolContext _dbContext;

        public GroupRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<Group>> GetAllAsync()
        {
            return await _dbContext.Set<Group>().Include(x => x.Course).Include(x => x.Students).Include(x => x.Curator).AsNoTracking().ToListAsync();
        }

        public async Task<Group> GetByIdAsync(int groupId)
        {
            return await _dbContext.Set<Group>().Include(x => x.Course).Include(x => x.Students).Include(x => x.Curator).FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public async Task<IReadOnlyList<Group>> GetBySearchAsync(string searchTerm)
        {
            var groups = (from entity in _dbContext.Groups
                          where (entity.Name + " " + entity.Course.Name).Contains(searchTerm)
                          select entity).Include(x => x.Course).Include(x => x.Students).Include(x => x.Curator).AsNoTracking().ToListAsync();

            return await groups;
        }

        public async Task<IReadOnlyList<Group>> GetByParentAsync(int parentId)
        {
            var groups = (from entity in _dbContext.Groups
                          where entity.CourseId == parentId
                          select entity).Include(x => x.Course).Include(x => x.Students).Include(x => x.Curator).AsNoTracking().ToListAsync();

            return await groups;
        }

        public async Task<Group> AddAsync(Group group)
        {
            _dbContext.Set<Group>().Add(group);
            await _dbContext.SaveChangesAsync();
            return group;
        }

        public async Task UpdateAsync(Group group)
        {
            _dbContext.Entry(group).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Group group)
        {
            _dbContext.Set<Group>().Remove(group);
            await _dbContext.SaveChangesAsync();
        }
    }
}
