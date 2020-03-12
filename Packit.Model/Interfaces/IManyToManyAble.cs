using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public interface IManyToManyAble
    {
        void SetLeftId(int id);
        void SetRightId(int id);
        int GetLeftId();
        int GetRightId();
    }
}
