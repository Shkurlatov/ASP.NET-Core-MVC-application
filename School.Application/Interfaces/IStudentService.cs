using School.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Service.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentModel>> GetStudentList();
        Task<StudentModel> GetStudentById(int studentId);
        Task<IEnumerable<StudentModel>> GetStudentByName(string name);
        Task<IEnumerable<StudentModel>> GetStudentByGroup(int groupId);
        Task<StudentModel> Create(StudentModel studentModel);
        Task Update(StudentModel studentModel);
        Task Delete(StudentModel studentModel);
    }
}
