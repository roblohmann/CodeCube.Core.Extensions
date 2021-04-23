using System;
using System.Collections.Generic;
using System.Collections.Specialized;

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
    }
}
