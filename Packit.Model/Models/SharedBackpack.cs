using Packit.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Model
{
    public class SharedBackpack : IDatabase
    {
        public int SharedBackpackId { get; set; }
        public int BackpackId { get; set; }
        public Backpack Backpack { get; set; }
        public User User { get; set; }

        public int GetId() => SharedBackpackId;
        public void SetId(int id) => SharedBackpackId = id;
    }
}
