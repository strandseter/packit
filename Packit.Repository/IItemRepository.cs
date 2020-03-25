using Packit.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Repository
{
    public interface IItemRepository
    {
        Item GetItem(int id);
        IEnumerable<Item> GetAllItems();
        Item Add(Item item);
        Item UpdateItem(Item updatedItem);
        Item Delete(Item item);
    }
}
