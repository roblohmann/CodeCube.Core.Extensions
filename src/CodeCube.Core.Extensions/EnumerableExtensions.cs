using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCube.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns a tuple of the provided type with the next and previous item included. Can be used to create paging effects.
        /// </summary>
        /// <typeparam name="T">The IEnumerable</typeparam>
        /// <returns>IEnumerable containing a Tuple with the current, next and previous item.</returns>
        public static IEnumerable<Tuple<T, T, T>> WithNextAndPrevious<T>(this IEnumerable<T> source)
        {
            // Actually yield "the previous two" as well as the current one - this
            // is easier to implement than "previous and next" but they're equivalent
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    yield break;
                }

                var lastButOne = iterator.Current;
                if (!iterator.MoveNext())
                {
                    yield break;
                }

                var previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    var current = iterator.Current;

                    yield return Tuple.Create(lastButOne, previous, current);

                    lastButOne = previous;
                    previous = current;
                }
            }
        }

        /// <summary>
        /// Perform the provided action for each item in the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumeration"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Splits a collection.
        /// </summary>
        /// <param name="source">The collection to split.</param>
        /// <param name="size">The size used to split the collection. Each resulting collection has this size.</param>
        /// <returns>A list containing subsets of the original list.<list type="T">list</list></returns>
        public static List<List<T>> Split<T>(this IList<T> source, int size)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
