using School.Core.Entities;
using School.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseByIdAsync(int courseId);
        Task<IEnumerable<Course>> GetCourseByNameAsync(string name);
    }
}
