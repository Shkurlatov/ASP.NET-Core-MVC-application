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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Student>> GetStudentListAsync()
        {
            var spec = new StudentByNameSpecification();
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
            var spec = new StudentByNameSpecification(name);
            return await GetAsync(spec);
        }

        public async Task<IEnumerable<Student>> GetStudentByGroupAsync(int groupId)
        {
            var spec = new StudentByGroupSpecification(groupId);
            return await GetAsync(spec);
        }
    }
}
