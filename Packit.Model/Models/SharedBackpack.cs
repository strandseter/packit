using Packit.Model.Models;

namespace Packit.Model
{
    public class SharedBackpack
    {
        public int SharedBackpackId { get; set; }
        public int BackpackId { get; set; }
        public Backpack Backpack { get; set; }

    }
}
