using School.Core.Entities;
using School.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentListAsync();
        Task<IEnumerable<Student>> GetStudentByLastNameAsync(string lastName);
        Task<IEnumerable<Student>> GetStudentByGroupAsync(int groupId);
    }
}
