using Packit.App.DataLinks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.Wrappers
{
    public class BackpackWithItemsWithImagesTripWrapper
    {
        public ObservableCollection<BackpackWithItemsWithImages>  BackpackWithItemsWithImages { get; set; }
        public TripImageWeatherLink TripImageWeatherLink { get; set; }
    }
}
