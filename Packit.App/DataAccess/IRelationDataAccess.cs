using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IRelationDataAccess<T1, T2>
    {
        Task<T2[]> GetEntitiesInEntityAsync(int id, string param);
        Task<bool> AddEntityToEntityAsync(int leftId, int rightId, string param1, string param2);
        Task<bool> DeleteEntityFromEntityAsync(int leftId, int rightId, string param1, string param2);
    }
}
