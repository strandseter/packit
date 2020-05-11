using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IBasicDataAccess<T>
    {
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T[]> GetAllAsync();
        Task<T> GetById(T entity);
        Task<T[]> GetAllWithChildEntities();
        Task<T> GetByIdWithChildEntities(T entity);
    }
}
