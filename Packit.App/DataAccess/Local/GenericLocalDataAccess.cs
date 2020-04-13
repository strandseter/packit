using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class GenericLocalDataAccess<T> : IDataAccess<T> where T : IDatabase
    {
        public Task<bool> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
