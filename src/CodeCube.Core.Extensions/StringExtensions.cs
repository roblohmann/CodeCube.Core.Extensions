using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using CodeCube.Core.Extensions.Helpers;

namespace CodeCube.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Creates an SHA-512 hashed string from an clear inputValue string
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string AsSha512(this string inputValue)
        {
            var sb = new StringBuilder();

            var sha512 = SHA512.Create();
            var inputBytes = Encoding.ASCII.GetBytes(inputValue);
            var hashBytes = sha512.ComputeHash(inputBytes);

            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Function to generate a friendly url.
        /// </summary>
        /// <param name="text">The text to create a slug from</param>
        /// <param name="maxLength">The maximum length allowed for the slug.</param>
        /// <returns>The slug</returns>
        public static string GenerateSlug(this string text, int maxLength = 255)
        {
            var str = text.ToLower();

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // convert multiple spaces/hyphens into one space      
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();

            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();

            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            //return the value
            return str;
        }

        /// <summary>
        /// Generates a friendly url
        /// </summary>
        /// <returns>A string representing an seo friendly url</returns>
        public static string AsFriendlyUrl(this string url)
        {
            // make the url lowercase
            var encodedUrl = (url ?? string.Empty).ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", string.Empty);

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        /// <summary>
        /// Strips all html-tags from an text and replaces them by an safely encoded string.
        /// </summary>
        /// <param name="input">The text to search trough</param>
        /// <returns>The original string with the value stripped</returns>
        public static string? StripTags(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            
            var regex = new Regex(@"(<\/?[^>]+>)");
            var str = input;

            foreach (Match match in regex.Matches(input))
            {
                str = ReplaceFirst(str.Trim(), match.Value, string.Empty);
            }
            return str.Trim();
        }

        /// <summary>
        /// Strips all html-tags from an text and replaces them by an safely encoded string.
        /// </summary>
        /// <param name="input">The text to search trough</param>
        /// <param name="allowedTags">The html-tags allowed in the string. Can be left out.</param>
        /// <returns>The original string with the value stripped</returns>
        public static string? StripTags(this string input, string[] allowedTags)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            
            var regex = new Regex(@"(<\/?[^>]+>)");
            var str = input;
            foreach (Match match in regex.Matches(input))
            {
                var str2 = match.Value.ToLower();
                var flag = false;

                foreach (var str3 in allowedTags)
                {
                    var index = -1;
                    if (index != 0)
                    {
                        index = str2.IndexOf('<' + str3 + '>', StringComparison.Ordinal);
                    }
                    if (index != 0)
                    {
                        index = str2.IndexOf('<' + str3 + ' ', StringComparison.Ordinal);
                    }
                    if (index != 0)
                    {
                        index = str2.IndexOf("</" + str3, StringComparison.Ordinal);
                    }
                    if (index == 0)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    str = ReplaceFirst(str.Trim(), match.Value, string.Empty);
                }
            }
            return str.Trim();
        }

        /// <summary>
        /// Removes all html from the specified string.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string? StripHtml(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return null;
            
            return RegularExpressions.Html.Replace(inputString, string.Empty);
        }

        /// <summary>
        /// Replaces the first occurence in the string
        /// </summary>
        /// <param name="haystack">The text to search through</param>
        /// <param name="needle">The Value to find in the text</param>
        /// <param name="replacement">The value to replace it with</param>
        /// <returns>The string with the value replaced</returns>
        public static string ReplaceFirst(this string haystack, string needle, string replacement)
        {
            return new Regex(Regex.Escape(needle)).Replace(haystack, replacement, 1);
        }


        /// <summary>
        /// Shortens the supplied text
        /// </summary>
        /// <param name="text">The text which needs to be shortened</param>
        /// <param name="amountOfCharacters">The amount of characters you want returned, default 300</param>
        /// <param name="addSuffix">Boolean indicating wether three dots should be added as suffix, default true</param>
        /// <param name="keepFullWords">Boolean indicating wether the shortended string should end with an full wordt, default true</param>
        /// <returns>An shortened string</returns>
        public static string ShortenText(this string text, int amountOfCharacters = 300, bool addSuffix = true, bool keepFullWords = true)
        {
            // replaces the truncated string to a ...
            var suffix = addSuffix ? "..." : string.Empty;

            if (amountOfCharacters <= 0) return text;

            //the maxlength without the suffix.
            var strLength = amountOfCharacters - suffix.Length;

            //If the length of the string is below 0, return the string
            if (strLength <= 0) return text;

            //If text is NULL of the length is smaller or equal to the length of the string, then just return it.
            if (text == null || text.Length <= amountOfCharacters) return text;

            //Should the shortened text end with an full word?
            if (keepFullWords)
            {
                //find the last occuring space
                var lastOccuringSpace = text.LastIndexOf(" ", amountOfCharacters, StringComparison.Ordinal);
                return $"{text.Substring(0, (lastOccuringSpace > 0) ? lastOccuringSpace : amountOfCharacters).Trim()}...";
            }

            //Cut the string
            var truncatedString = text.Substring(0, strLength);

            //Remove trailing spaces
            truncatedString = truncatedString.TrimEnd();

            //Return the string
            return string.Format("{0}" + suffix, truncatedString);
        }

        /// <summary>
        /// Makes an uppercase of the first character in the string this extension method is called on.
        /// </summary>
        /// <param name="value">The string from which the first char need to be uppercased</param>
        /// <returns>String with uppercased first char</returns>
        public static string UppercaseFirstChar(this string value)
        {
            if (value.Length > 0)
            {
                var array = value.ToCharArray();
                array[0] = char.ToUpper(array[0]);

                return new string(array);
            }

            return value;
        }

        /// <summary>
        /// Test if the string is a valid Guid
        /// </summary>
        /// <returns>True if an Guid, otherwise false</returns>
        public static bool IsValidGuid(this string str)
        {
            return Guid.TryParse(str, out _);
        }

        /// <summary>
        /// Try to parse a string value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="stringValue">The string value to parse.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum TryParseEnum<TEnum>(this string stringValue) where TEnum : struct
        {
            return EnumHelper.TryParseEnum<TEnum>(stringValue, default(TEnum));
        }

        /// <summary>
        /// Try to parse a string value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="stringValue">The string value to parse.</param>
        /// <param name="defaultValue">The default enum value to return if the value can't be parsed.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum TryParseEnum<TEnum>(this string stringValue, TEnum defaultValue) where TEnum : struct
        {
            return EnumHelper.TryParseEnum<TEnum>(stringValue, defaultValue);
        }

        /// <summary>
        /// Try to parse a string value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="stringValue">The string value to parse.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum? TryParseEnumOptional<TEnum>(this string stringValue) where TEnum : struct
        {
            return EnumHelper.TryParseEnumOptional<TEnum>(stringValue, null);
        }

        /// <summary>
        /// Try to parse a string value to the provided enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum which the string should be parsed into.</typeparam>
        /// <param name="stringValue">The string value to parse.</param>
        /// <param name="defaultValue">The default enum value to return if the value can't be parsed.</param>
        /// <returns>If parsing succeeds, then the parsed enum value will be returned. Otherwise the defaultvalue will be returned.</returns>
        public static TEnum? TryParseEnumOptional<TEnum>(this string stringValue, TEnum defaultValue) where TEnum : struct
        {
            return EnumHelper.TryParseEnumOptional<TEnum>(stringValue, defaultValue);
        }
    }
}
