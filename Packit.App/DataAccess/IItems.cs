using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public interface IItems : IDataAccess<Item>
    {
        //Declare methods that are not possible to make generic here.
    }
}
