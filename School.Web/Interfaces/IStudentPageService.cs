using School.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Interfaces
{
    public interface IStudentPageService
    {
        Task<IEnumerable<StudentViewModel>> GetStudents(string name);
        Task<StudentViewModel> GetStudentById(int studentId);
        Task<IEnumerable<StudentViewModel>> GetStudentByGroup(int groupId);
        Task<IEnumerable<GroupViewModel>> GetGroups();
        Task<StudentViewModel> CreateStudent(StudentViewModel studentViewModel);
        Task UpdateStudent(StudentViewModel studentViewModel);
        Task DeleteStudent(StudentViewModel studentViewModel);
    }
}
