using School.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Interfaces
{
    public interface IIndexPageService
    {
        Task<IEnumerable<StudentViewModel>> GetStudents();
        Task<IEnumerable<GroupViewModel>> GetGroups();
    }
}
