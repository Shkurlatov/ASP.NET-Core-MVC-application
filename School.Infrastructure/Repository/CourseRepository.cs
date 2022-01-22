using School.Core.Entities;
using School.Core.Repositories;
using School.Core.Specifications;
using School.Infrastructure.Data;
using School.Infrastructure.Repository.Base;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Infrastructure.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            var spec = new CourseByIdSpecification(courseId);
            var course = (await GetAsync(spec)).FirstOrDefault();
            return course;
        }

        public async Task<IEnumerable<Course>> GetCourseByNameAsync(string name)
        {
            var spec = new CourseByNameSpecification(name);
            return await GetAsync(spec);
        }
    }
}
