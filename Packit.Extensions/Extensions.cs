using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Packit.Extensions
{
    public static class Extensions
    {
        public static IList<T> DeepClone<T>(this IList<T> list) => JsonConvert.DeserializeObject<T[]>(JsonConvert.SerializeObject(list));

        public static T DeepClone<T>(this T a) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(a));
    }
}
