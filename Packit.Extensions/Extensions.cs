using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Packit.Extensions
{
    //I would have split these methods into seperate classes if I hade more extensions. But for know i think it looks most tidy to have it like this.
    public static class Extensions
    {
        public static IList<T> DeepClone<T>(this IList<T> list) => JsonConvert.DeserializeObject<T[]>(JsonConvert.SerializeObject(list));

        public static T DeepClone<T>(this T a) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(a));

        public static int RemoveAll<T>(this ICollection<T> collection, Func<T, bool> condition)
        {
            var entityToRemove = collection.Where(condition).ToList();

            foreach (var itemToRemove in entityToRemove)
                collection.Remove(itemToRemove);

            return entityToRemove.Count;
        }
    }
}
