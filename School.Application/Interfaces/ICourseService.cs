using School.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetCourseList();
    }
}
