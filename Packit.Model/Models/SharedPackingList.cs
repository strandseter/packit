using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class SharedBackpack : IDatabaseExistable
    {
        public int SharedBackpackId { get; set; }
        public Backpack Backpack { get; set; }
        public User User { get; set; }

        public int Id()
        {
            return SharedBackpackId;
        }
    }
}
