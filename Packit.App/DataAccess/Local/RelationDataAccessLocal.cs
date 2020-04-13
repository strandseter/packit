using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class RelationDataAccessLocal<T1, T2> : IRelationDataAccess<T1, T2>
    {
        public Task<bool> AddEntityToEntity(int leftId, int rightId, string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntityFromEntity(int leftId, int rightId, string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Task<T2[]> GetEntitiesInEntity(int id, string param)
        {
            throw new NotImplementedException();
        }
    }
}
