using Packit.App.DataAccess;
using Packit.App.DataAccess.Http;
using Packit.App.DataAccess.Local;
using Packit.App.Services;

namespace Packit.App.Factories
{
    public static class CustomTripDataAccessFactory
    {
        public static ICustomTripDataAccess Create() => InternetConnectionService.IsConnectedMock() ? new CustomTripDataAccessHttp() : (ICustomTripDataAccess)new CustomTripDataAccessLocal();
    }
}
