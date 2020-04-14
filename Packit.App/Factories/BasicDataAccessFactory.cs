using Packit.App.DataAccess;
using Packit.App.Services;
using Packit.Model.Models;

namespace Packit.App.Factory
{
    public class BasicDataAccessFactory<T> where T : IDatabase
    {
        public IBasicDataAccess<T> CreateBasicDataAccess() => InternetConnectionService.IsConnected() ? new BasicDataAccessHttp<T>() : (IBasicDataAccess<T>)new BasicDataAccessLocal<T>();
    }
}
