using School.Core.Entities;
using School.Core.Repositories;
using School.Core.Specifications;
using School.Infrastructure.Data;
using School.Infrastructure.Repository.Base;
using System.Linq;
using System.Threading.Tasks;

namespace School.Infrastructure.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext dbContext) : base(dbContext)
        {
        }

        public async Task<Course> GetCourseWithGroupsAsync(int courseId)
        {
            var spec = new CourseWithGroupsSpecification(courseId);
            var course = (await GetAsync(spec)).FirstOrDefault();
            return course;
        }
    }
}
