using School.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<Course> GetCourseByIdAsync(int courseId);
        Task<IEnumerable<Course>> GetCourseByNameAsync(string searchTerm);
    }
}
