using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface ICustomBackpackDataAccess
    {
        Task<Backpack[]> GetAllSharedBackpacksByUserAsync();
        Task<Backpack[]> GetAllSharedBackpacksAsync();
    }
}
