using School.Domain.Entities;
using School.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentListAsync();
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentByNameAsync(string name);
        Task<IEnumerable<Student>> GetStudentByGroupAsync(int groupId);
    }
}
