using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App
{
    public interface IWindowDataTransfer
    {
        void ChangeWindow<T>(object o);
    }
}
