using School.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentListAsync();
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentByNameAsync(string searchTerm);
        Task<IEnumerable<Student>> GetStudentByGroupAsync(int groupId);
    }
}
