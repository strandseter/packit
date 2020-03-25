using Packit.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packit.Repository
{
    public class WebApiItemRepository : IItemRepository
    {
        public Item Add(Item item)
        {
            throw new NotImplementedException();
        }

        public Item Delete(Item item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public Item GetItem(int id)
        {
            return new Item() { Title = "RepositoryDummyItem", Description = "This is a test", ImageStringName = "Image.png" };
        }

        public Item UpdateItem(Item updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
