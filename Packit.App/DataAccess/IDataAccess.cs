using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.DataAccess
{
    public interface IDataAccess<T>
    {
        Task<bool> Add(T entity);
        Task<bool> Edit(T entity);
        Task<bool> Delete(T entity);
        Task<T[]> GetAll();
    }
}
