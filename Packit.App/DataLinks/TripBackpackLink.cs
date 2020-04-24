using Packit.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.DataLinks
{
    public class TripBackpackLink
    {
        public Trip Trip { get; set; }
        public ObservableCollection<Backpack> Backpacks { get; } = new ObservableCollection<Backpack>();
    }
}
