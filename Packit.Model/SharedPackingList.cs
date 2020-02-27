using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class SharedPackingList
    {
        public int SharedPackingListId { get; set; }
        public PackingList PackingList { get; set; }
        public User User { get; set; }
    }
}
