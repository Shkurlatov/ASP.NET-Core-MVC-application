using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly SchoolContext _dbContext;

        public Repository(SchoolContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public abstract Task<IReadOnlyList<T>> GetAllAsync();

        public abstract Task<T> GetByIdAsync(int id);

        public abstract Task<IReadOnlyList<T>> GetBySearchAsync(string searchTerm);

        public abstract Task<IReadOnlyList<T>> GetByParentAsync(int parentId);

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
