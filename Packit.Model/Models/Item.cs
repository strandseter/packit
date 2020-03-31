
using Packit.Model.Interfaces;
using Packit.Model.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Packit.Model
{
    public class Item : BaseInformation, IDatabase
    {
        public int ItemId { get; set; }
        public virtual ICollection<ItemBackpack> Backpacks { get; set;}

        public Item()
        {
            Backpacks = new List<ItemBackpack>();
        }

        public Item(string title, string description, string imageStringName, User user)
            :base(title, description, imageStringName, user)
        {
            Backpacks = new List<ItemBackpack>();
        }

        public override string ToString() => $"{Title} {ItemId}";
        public int GetId() => ItemId;
    }
}
