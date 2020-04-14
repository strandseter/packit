using Packit.Model.Models;

namespace Packit.Model
{
    public class SharedBackpack : IDatabase
    {
        public int SharedBackpackId { get; set; }
        public int BackpackId { get; set; }
        public Backpack Backpack { get; set; }
        public User User { get; set; }

        public int Id { get => SharedBackpackId; set => SharedBackpackId = value; }
    }
}
