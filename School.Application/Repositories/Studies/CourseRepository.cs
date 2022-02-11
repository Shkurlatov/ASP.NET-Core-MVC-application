using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly SchoolContext _dbContext;

        public CourseRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<Course>> GetAllAsync()
        {
            return await _dbContext.Set<Course>().Include(x => x.Groups).AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int courseId)
        {
            return await _dbContext.Set<Course>().Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == courseId);
        }

        public async Task<IReadOnlyList<Course>> GetBySearchAsync(string searchTerm)
        {
            var courses = (from entity in _dbContext.Courses
                         where entity.Name.Contains(searchTerm)
                         select entity).Include(x => x.Groups).AsNoTracking().ToListAsync();

            return await courses;
        }

        public Task<IReadOnlyList<Course>> GetByParentAsync(int parentId)
        {
            // not used for this entity
            throw new System.NotImplementedException();
        }

        public async Task<Course> AddAsync(Course course)
        {
            _dbContext.Set<Course>().Add(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }

        public async Task UpdateAsync(Course course)
        {
            _dbContext.Entry(course).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            _dbContext.Set<Course>().Remove(course);
            await _dbContext.SaveChangesAsync();
        }
    }
}
