using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodeCube.Core.Extensions
{
    /// <summary>
    /// Class with extensionmethods for enums.
    /// </summary>
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

        /// <summary>
        /// Get the description value from the Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns>The description of the enum value, or the name of the value when no description is set. Returns <c>null</c> when <typeparamref name="T"/> is not an enum.</returns>
        public static string? GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString() ?? string.Empty;
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes.Length > 0)
                {
                    description = ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return description;
        }
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