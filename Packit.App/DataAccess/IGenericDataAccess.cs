using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IGenericDataAccess<T>
    {
        Task<bool> Add(T entity, string uriExtension);
        Task<bool> Edit(T entity, int id, string uriExtension);
        Task<bool> Delete(T entity, string uriExtension);
        Task<T[]> GetAll(string uriExtension);
    }
}
