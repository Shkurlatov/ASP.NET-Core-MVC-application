using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Specifications;
using School.Infrastructure.Data;

namespace School.Application.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Student>> GetStudentListAsync()
        {
            var spec = new StudentBySearchSpecification();
            return await GetAsync(spec);
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var spec = new StudentByIdSpecification(studentId);
            var student = (await GetAsync(spec)).FirstOrDefault();
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudentByNameAsync(string name)
        {
            var spec = new StudentBySearchSpecification(name);
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Student>> GetStudentByGroupAsync(int groupId)
        {
            var spec = new StudentByGroupSpecification(groupId);
            return await GetAsync(spec);
        }
    }
}
