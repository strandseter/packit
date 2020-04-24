using Packit.App.DataAccess;
using Packit.App.Services;
using Packit.Model.Models;

namespace Packit.App.Factories
{
    public class BasicDataAccessFactory<T> where T : IDatabase
    {
        public IBasicDataAccess<T> Create() => InternetConnectionService.IsConnected() ? new BasicDataAccessHttp<T>() : (IBasicDataAccess<T>)new BasicDataAccessLocal<T>();
    }
}
