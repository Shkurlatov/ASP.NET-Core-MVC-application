using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IReadOnlyList<Course>> GetAllAsync()
        {
            return await _dbContext.Set<Course>().Include(x => x.Groups).AsNoTracking().ToListAsync();
        }

        public override async Task<Course> GetByIdAsync(int courseId)
        {
            return await _dbContext.Set<Course>().Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public override async Task<IReadOnlyList<Course>> GetBySearchAsync(string searchTerm)
        {
            var courses = (from entity in _dbContext.Courses
                         where entity.Name.Contains(searchTerm)
                         select entity).Include(x => x.Groups).AsNoTracking().ToListAsync();

            return await courses;
        }

        public override Task<IReadOnlyList<Course>> GetByParentAsync(int parentId)
        {
            // not used for this entity
            throw new System.NotImplementedException();
        }
    }
}
