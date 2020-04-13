using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IBasicDataAccessApi<T>
    {
        Task<bool> Add(T entity);
        Task<bool> Edit(T entity);
        Task<bool> Delete(T entity);
        Task<T[]> GetAll();
    }
}
