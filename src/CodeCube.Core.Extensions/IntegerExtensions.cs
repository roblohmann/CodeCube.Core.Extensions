using CodeCube.Core.Extensions.Helpers;

namespace CodeCube.Core.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Try to parse an integer value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="value">The integer value to parse.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum TryParseEnum<TEnum>(this int value) where TEnum : struct
        {
            return EnumHelper.TryParseEnum<TEnum>(value, default(TEnum));
        }

        /// <summary>
        /// Try to parse an integer value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="value">The integer value to parse.</param>
        /// <param name="defaultValue">The default enum value to return if the value can't be parsed.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum TryParseEnum<TEnum>(this int value, TEnum defaultValue) where TEnum : struct
        {
            return EnumHelper.TryParseEnum<TEnum>(value, defaultValue);
        }

        /// <summary>
        /// Try to parse an integer value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="value">The integer value to parse.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum? TryParseEnumOptional<TEnum>(this int value) where TEnum : struct
        {
            return EnumHelper.TryParseEnumOptional<TEnum>(value, null);
        }

        /// <summary>
        /// Try to parse an integer value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="value">The integer value to parse.</param>
        /// <param name="defaultValue">The default enum value to return if the value can't be parsed.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum? TryParseEnumOptional<TEnum>(this int value, TEnum defaultValue) where TEnum : struct
        {
            return EnumHelper.TryParseEnumOptional<TEnum>(value, defaultValue);
        }
    }
}
