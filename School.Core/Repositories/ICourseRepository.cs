using School.Domain.Entities;
using School.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseByIdAsync(int courseId);
        Task<IEnumerable<Course>> GetCourseByNameAsync(string name);
    }
}
