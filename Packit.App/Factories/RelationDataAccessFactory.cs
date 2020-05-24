using Packit.App.DataAccess;
using Packit.App.Services;

namespace Packit.App.Factories
{
    public class RelationDataAccessFactory<T1, T2>
    {
        public IRelationDataAccess<T1, T2> Create() => InternetConnectionService.IsConnectedMock() ? new RelationDataAccessHttp<T1, T2>() : (IRelationDataAccess<T1, T2>)new RelationDataAccessLocal<T1, T2>();
    }
}
