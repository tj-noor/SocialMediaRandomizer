using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvRandomizer
{
    public static class Extensions
    {
        /// <summary>
        /// Picks a number of random element(s) from the list
        /// </summary>
        /// <param name="source">Provided List</param>
        /// <param name="count">how many do you want?</param>
        /// <returns>a list of elements</returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            IEnumerable<T> enumerable = source as T[] ?? source.ToArray();
            if (enumerable.Count() < count)
            {
                throw new Exception($"PickRandom Source does not have {count} item(s).");
            }

            return enumerable.Shuffle().Take(count);
        }

        /// <summary>
        /// Changes order in the list
        /// </summary>
        /// <returns>your shuffled list</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) => source.OrderBy(x => Guid.NewGuid());
    }
}