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
    public class StudentRepository : IRepository<Student>
    {
        private readonly SchoolContext _dbContext;

        public StudentRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<IReadOnlyList<Student>> GetAllAsync()
        {
            return await _dbContext.Set<Student>().Include(x => x.Group).AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int studentId)
        {
            return await _dbContext.Set<Student>().Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<IReadOnlyList<Student>> GetBySearchAsync(string searchTerm)
        {
            var students = (from entity in _dbContext.Students
                           where (entity.FirstName + " " + entity.LastName + " " + entity.Group.Name).Contains(searchTerm)
                           select entity).Include(x => x.Group).AsNoTracking().ToListAsync();

            return await students;
        }

        public async Task<IReadOnlyList<Student>> GetByParentAsync(int parentId)
        {
            var students = (from entity in _dbContext.Students
                            where entity.GroupId == parentId
                            select entity).Include(x => x.Group).AsNoTracking().ToListAsync();

            return await students;
        }

        public async Task<Student> AddAsync(Student entity)
        {
            _dbContext.Set<Student>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Student entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student entity)
        {
            _dbContext.Set<Student>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
