using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces.Users
{
    public interface IUserService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task Update(T model);
        Task Delete(T model);
    }
}
