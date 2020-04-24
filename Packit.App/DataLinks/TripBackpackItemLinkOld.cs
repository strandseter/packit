using Packit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataLinks
{
    public class TripBackpackItemLinkOld
    {
        public Trip Trip { get; set; }
        public ObservableCollection<BackpackItemLinkOld> BackpackItems { get; } = new ObservableCollection<BackpackItemLinkOld>();
    }
}
