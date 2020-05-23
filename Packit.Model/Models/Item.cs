
using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Packit.Model
{
    public class Item : BaseInformation, ISerializable
    {
        private Check check;

        public int ItemId { get; set; }
        public virtual ICollection<ItemBackpack> Backpacks { get; } = new List<ItemBackpack>();
        public ICollection<Check> Checks { get; } = new List<Check>();
        [NotMapped]
        public Check Check 
        { 
            get => check;
            set => Set(ref check, value);
        }

        public Item()
        {
        }

        public override int GetId() => ItemId;

        public override void SetId(int value) => ItemId = value;

        public override string ToString() => $"{Title} {ItemId}";

        public int GetUserId() => UserId;

        public void GetObjectData(SerializationInfo info, StreamingContext context) => throw new NotImplementedException();
    }
}
