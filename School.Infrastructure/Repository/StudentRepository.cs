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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Student>> GetStudentListAsync()
        {
            var spec = new StudentWithGroupSpecification();
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Student>> GetStudentByLastNameAsync(string lastName)
        {
            var spec = new StudentWithGroupSpecification(lastName);
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Student>> GetStudentByGroupAsync(int groupId)
        {
            return await _dbContext.Students
                .Where(x => x.GroupId == groupId)
                .ToListAsync();
        }
    }
}
