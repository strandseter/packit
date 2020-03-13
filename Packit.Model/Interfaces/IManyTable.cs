using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model.Interfaces
{
    public interface IManyTable
    {
        IOneTable GetForeignObject();
    }
}
