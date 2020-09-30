using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueriesPractice
{
    static class MyOwnLinq
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // var result = new List<T>();

            // Switching to yield return
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    // result.Add(item);
                    yield return item;
                }
            }

            // return result;
        }
    }
}
