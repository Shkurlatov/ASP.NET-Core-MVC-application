using School.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Interfaces
{
    public interface ICoursePageService
    {
        Task<IEnumerable<CourseViewModel>> GetCourses(string name, int courseId);
    }
}
