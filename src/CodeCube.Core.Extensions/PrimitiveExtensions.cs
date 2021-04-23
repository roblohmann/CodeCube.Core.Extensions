using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeCube.Core.Extensions
{
    public static class PrimitiveExtensions
    {
        /// <summary>
        /// Converts the object to a valid string value.
        /// </summary>
        /// <returns>The string value of the object. Defaults to empty string.</returns>
        public static string ToNullSafeString(this object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Is this type nullable?
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if the type is nullable, otherwise false.</returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Is this type an IEnumerable.
        /// </summary>
        /// <param name="type">The type to chec</param>
        /// <returns>True if the type is an IEnumerable, otherwise false.</returns>
        public static bool IsEnumerableType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IEnumerable));
        }

        /// <summary>
        /// Is this type an Dictionary?
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if the type is an dictionary, otherwise false.</returns>
        public static bool IsDictionaryType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                return true;

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType);
            var baseDefinitions = genericInterfaces.Select(t => t.GetGenericTypeDefinition());
            return baseDefinitions.Any(t => t == typeof(IDictionary<,>));
        }

        /// <summary>
        /// Get's the dictionary from the provided type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetDictionaryType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                return type;

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            return genericInterfaces.FirstOrDefault();
        }
    }
}
