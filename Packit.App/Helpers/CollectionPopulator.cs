using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packit.App.Helpers
{
    public class CollectionPopulator<T>
    {
        private ICollection<T> collection;
        private T[] entities;

        public CollectionPopulator(ICollection<T> collection, T[] entities)
        {
            this.collection = collection;
            this.entities = entities;
        }

        public void Populate()
        {
            foreach (var entity in entities)
                collection.Add(entity);
        }
    }
}
