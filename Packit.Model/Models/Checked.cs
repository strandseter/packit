using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model.Models
{
    public class Checked
    {
        public Item Item { get; set; }
        public int ItemdId { get; set; }
        public Backpack Backpack { get; set; }
        public int BackpackId { get; set; }
        public bool IsChecked { get; set; }
    }
}
