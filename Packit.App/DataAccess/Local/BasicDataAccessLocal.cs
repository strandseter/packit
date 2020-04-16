using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class BasicDataAccessLocal<T> : IBasicDataAccess<T> where T : IDatabase
    {
        public Task<bool> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T[]> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
