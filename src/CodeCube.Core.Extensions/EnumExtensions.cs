using System;
using System.Collections.Generic;

namespace CodeCube.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get a list of all uniquely enabled flags for the specified <typeparamref name="T"/> enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flags"></param>
        /// <returns>List of <typeparamref name="T"/> where the flag is set.</returns>
        public static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
        {
            foreach (Enum value in Enum.GetValues(flags.GetType()))
                if (flags.HasFlag(value) && !Equals((int)(object)value, 0))
                    yield return (T)value;
        }

        //public static List<TEnum> ToList<TEnum>(this TEnum values) where TEnum : Enum
        //{
        //    return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        //}

        //public static IEnumerable<TEnum> AsIEnumerable<TEnum>(this TEnum values) where TEnum : Enum
        //{
        //    return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        //}
    }
}
