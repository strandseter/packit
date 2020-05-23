using Packit.App.DataLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.Wrappers
{
    public class BackpackWithItemsTripImageWeatherWrapper
    {
        public BackpackWithItemsWithImages Backpack { get; set; }
        public TripImageWeatherLink Trip { get; set; }
    }
}
