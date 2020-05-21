using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Extensions
{
    public static class Extensions
    {
        public static IList<T> DeepClone<T>(this IList<T> list) => JsonConvert.DeserializeObject<T[]>(JsonConvert.SerializeObject(list));

        public static async Task<IList<T>> DeepCloneAsync<T>(this IList<T> list) => await Task.Run(() => JsonConvert.DeserializeObject<T[]>(JsonConvert.SerializeObject(list)));
    }
}
