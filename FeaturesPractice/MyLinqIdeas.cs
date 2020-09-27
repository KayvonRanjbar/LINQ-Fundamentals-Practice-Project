using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesPractice.Linq
{
    public static class MyLinqIdeas
    {
        /// <summary>
        /// A crazy method that returns the count of items in a sequence MINUS ONE.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static int CountMinusOne<T>(this IEnumerable<T> sequence)
        {
            int countMinusOne = 0;
            foreach (var item in sequence)
            {
                countMinusOne++;
            }
            countMinusOne -= 1;
            return countMinusOne;
        }
    }
}
