using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IReadOnlyList<Student>> GetAllAsync()
        {
            return await _dbContext.Set<Student>().Include(x => x.Group).AsNoTracking().ToListAsync();
        }

        public override async Task<Student> GetByIdAsync(int studentId)
        {
            return await _dbContext.Set<Student>().Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public override async Task<IReadOnlyList<Student>> GetBySearchAsync(string searchTerm)
        {
            var students = (from entity in _dbContext.Students
                           where (entity.FirstName + " " + entity.LastName + " " + entity.Group.Name).Contains(searchTerm)
                           select entity).Include(x => x.Group).AsNoTracking().ToListAsync();

            return await students;
        }

        public override async Task<IReadOnlyList<Student>> GetByParentAsync(int parentId)
        {
            var students = (from entity in _dbContext.Students
                            where entity.GroupId == parentId
                            select entity).Include(x => x.Group).AsNoTracking().ToListAsync();

            return await students;
        }
    }
}
