using System;

namespace CodeCube.Core.Extensions.Helpers
{
    /// <summary>
    /// Helper class with enum methods.
    /// </summary>
    internal static class EnumHelper
    {
        internal static TEnum? TryParseEnumOptional<TEnum>(int value, TEnum? defaultValue) where TEnum : struct
        {
            return TryParseEnumOptional(value.ToString(), defaultValue);
        }

        internal static TEnum TryParseEnum<TEnum>(int value, TEnum defaultValue) where TEnum : struct
        {
            return TryParseEnum(value.ToString(), defaultValue);
        }

        /// <summary>
        /// Try to parse a string value to the provided enum.
        /// </summary>
        /// <remarks>Enum is always parsed case-insensitive!</remarks>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="defaultValue">The default enum value to return if the value can't be parsed. Can be NULL</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        internal static TEnum? TryParseEnumOptional<TEnum>(string value, TEnum? defaultValue) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value) && defaultValue.HasValue) return defaultValue.Value;
            if (string.IsNullOrWhiteSpace(value)) return default(TEnum); 

            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// Try to parse a string value to the provided enum.
        /// </summary>
        /// <remarks>Enum is always parsed case-insensitive!</remarks>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <param name="defaultValue">The default enum value to return if the value can't be parsed. Can be NULL</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        internal static TEnum TryParseEnum<TEnum>(string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value)) return default;

            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }

            return defaultValue;
        }
    }
}