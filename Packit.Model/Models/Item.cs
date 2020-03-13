using Packit.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Item : BaseInformation, IManyTable
    {
        public int ItemId { get; set; }
        public User User { get; set; }
        public virtual ICollection<ItemBackpack> Backpacks { get; set;}

        public Item()
        {
            Backpacks = new List<ItemBackpack>();
        }

        public Item(string title, string description, string imageStringName)
            :base(title, description, imageStringName)
        {
            Backpacks = new List<ItemBackpack>();
        }

        public override string ToString()
        {
            return $"{Title} {ItemId}";
        }

        public int Id()
        {
            return ItemId;
        }

        public IOneTable GetForeignObject()
        {
            return null;
        }
    }
}
