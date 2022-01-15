using School.Core.Entities;
using School.Core.Repositories.Base;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseWithGroupsAsync(int courseId);
    }
}
