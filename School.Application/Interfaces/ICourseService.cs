using School.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Service.Interfaces
{
    public interface ICourseService
    {
        Task<CourseModel> GetCourseById(int courseId);
        Task<IEnumerable<CourseModel>> GetCourseList();
        Task<IEnumerable<CourseModel>> GetCourseByName(string name);       
    }
}
