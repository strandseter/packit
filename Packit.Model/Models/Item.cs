
using Packit.Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packit.Model
{
    public class Item : BaseInformation, IDatabase
    {
        public int ItemId { get; set; }
        public virtual ICollection<ItemBackpack> Backpacks { get; } = new List<ItemBackpack>();
        public ICollection<Check> Checks { get; } = new List<Check>();
        [NotMapped]
        public bool IsChecked { get; set; }

        public Item() => ImageStringName = $"image{ItemId}";

        public override int GetId() => ItemId;

        public override void SetId(int value) => ItemId = value;

        public override string ToString() => $"{Title} {ItemId}";

        public int GetUserId() => UserId;
    }
}
