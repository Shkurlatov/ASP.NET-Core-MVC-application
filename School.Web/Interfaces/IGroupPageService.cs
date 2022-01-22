using School.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Interfaces
{
    public interface IGroupPageService
    {
        Task<IEnumerable<GroupViewModel>> GetGroups(string name, int groupId);
        Task<GroupViewModel> GetGroupById(int groupId);
        Task<IEnumerable<GroupViewModel>> GetGroupByCourse(int courseId);
        Task<IEnumerable<CourseViewModel>> GetCourses();
        Task<GroupViewModel> CreateGroup(GroupViewModel groupViewModel);
        Task UpdateGroup(GroupViewModel groupViewModel);
        Task DeleteGroup(GroupViewModel groupViewModel);
    }
}
