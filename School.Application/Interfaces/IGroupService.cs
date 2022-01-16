using School.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Application.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupModel>> GetGroupList();
        Task<GroupModel> GetGroupById(int groupId);
        Task<IEnumerable<GroupModel>> GetGroupByName(string name);
        Task<IEnumerable<GroupModel>> GetGroupByCourse(int courseId);
        Task<GroupModel> Create(GroupModel groupModel);
        Task Update(GroupModel groupModel);
        Task Delete(GroupModel groupModel);
    }
}
