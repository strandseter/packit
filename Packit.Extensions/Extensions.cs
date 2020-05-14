using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Packit.Extensions
{
    public static class Extensions
    {
        public static IList<T> DeepClone<T>(this IList<T> list) => JsonConvert.DeserializeObject<T[]>(JsonConvert.SerializeObject(list));
    }
}
