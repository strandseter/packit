using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model.Interfaces
{
    public interface IOneTable
    {
        string GetIdentityId();
        int GetPrimaryId();
    }
}
