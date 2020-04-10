using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess
{
    public interface IGenericDataAccess<T>
    {
        Task<bool> Add(T entity, string uriExtension);
        Task<bool> Edit(T entity, string uriExtension);
        Task<bool> Delete(T entity, string uriExtension);
        Task<T[]> GetAll(string uriExtension);
    }
}
