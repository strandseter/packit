using Packit.App.DataAccess;
using Packit.App.Services;
using Packit.Model.Models;

namespace Packit.App.Factory
{
    public class BasicDataAccessFactory<T> where T : IDatabase
    {
        public IBasicDataAccessHttp<T> CreateBasicDataAccess() => InternetConnectionService.IsConnected() ? new BasicDataAccessHttp<T>() : (IBasicDataAccessHttp<T>)new BasicDataAccessLocal<T>();
    }
}
