using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CodeCube.Core.Extensions
{
    /// <summary>
    /// Class with extensionsmethods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert a dictionary to a namevaluecollection.
        /// </summary>
        /// <param name="dictionary">The dictionary to convert.</param>
        /// <typeparam name="TKey">The strongtyped key for the dictionary.</typeparam>
        /// <typeparam name="TValue">The strongtyped value for the ditionary. Can be null.</typeparam>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var nameValueCollection = new NameValueCollection();

            foreach (var kvp in dictionary)
            {
                if (kvp.Value == null) continue;

                var value = kvp.Value.ToString();
                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }

        /// <summary>
        /// Extension method to merge one collection into another.
        /// </summary>
        /// <param name="target">The target collection.</param>
        /// <param name="source">The source collection.</param>
        public static void AddRange<T, S>(this Dictionary<T, S> target, Dictionary<T, S> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Empty collection");
            }

            foreach (var item in source)
            {
                if (!target.ContainsKey(item.Key))
                {
                    target.Add(item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// Extension method to execute a where on a dictionary and return the result as a dictionary with the
        /// same type of key van value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="instance">The dictionary to filter.</param>
        /// <param name="predicate">The actual filter.</param>
        /// <returns>Dictionary of type <see cref="Dictionary{TKey,TValue}"/></returns>
        public static Dictionary<TKey, TValue> Where<TKey, TValue>(this Dictionary<TKey, TValue> instance, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            return Enumerable.Where(instance, predicate)
                             .ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
