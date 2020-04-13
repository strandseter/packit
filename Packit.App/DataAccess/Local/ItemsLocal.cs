using Packit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataAccess
{
    public class ItemsLocal : GenericLocalDataAccess<Item>, IItems
    {
        //Implement methods that are not possible to make generic here.
    }
}
