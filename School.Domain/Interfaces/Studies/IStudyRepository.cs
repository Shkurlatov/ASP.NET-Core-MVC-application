﻿using School.Domain.Entities.Studies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces.Studies
{
    public interface IStudyRepository<T> where T : Entity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetBySearchAsync(string searchTerm);
        Task<IReadOnlyList<T>> GetByParentAsync(int parentId);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
