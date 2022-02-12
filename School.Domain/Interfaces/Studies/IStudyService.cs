using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Domain.Interfaces.Studies
{
    public interface IStudyService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetBySearch(string searchTerm);
        Task<IEnumerable<T>> GetByParent(int parentId);
        Task<T> GetById(int id);
        Task Create(T model);
        Task  Update(T model);
        Task  Delete(T model);
    }
}
