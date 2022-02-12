using School.Domain.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces.Users
{
    public interface IUserRepository<T> where T : User
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
