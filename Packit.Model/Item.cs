using System;

namespace Packit.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{Title} {ItemId}";
        }
    }
}
