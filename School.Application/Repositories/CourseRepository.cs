using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using School.Domain.Entities;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class CourseRepository : Repository<Course>//, ICourseRepository
    {
        public CourseRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        //public async Task<Course> GetCourseByIdAsync(int courseId)
        //{
        //    var spec = new CourseByIdSpecification(courseId);
        //    var course = (await GetAsync(spec)).FirstOrDefault();
        //    return course;
        //}

        //public async Task<IEnumerable<Course>> GetCourseByNameAsync(string name)
        //{
        //    var spec = new CourseBySearchSpecification(name);
        //    return await GetAsync(spec);
        //}
    }
}
