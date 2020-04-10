
using Packit.Model.Models;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Item : BaseInformation, IDatabase
    {
        public int ItemId { get; set; }

        public virtual ICollection<ItemBackpack> Backpacks { get; } = new List<ItemBackpack>();

        public Item()
        {
        }

        public override string ToString() => $"{Title} {ItemId}";
        public int GetId() => ItemId;
        public void SetId(int id) => ItemId = id;
    }
}
