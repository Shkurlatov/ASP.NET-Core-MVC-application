using School.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<IEnumerable<Group>> GetGroupListAsync();
        Task<IEnumerable<Group>> GetGroupByNameAsync(string searchTerm);
        Task<IEnumerable<Group>> GetGroupByCourseAsync(int courseId);
    }
}
