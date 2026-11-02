using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeCube.Core.Extensions
{
    /// <summary>
    /// Class with extensionmethods to serialize and deserialize objects using <see cref="JsonSerializer"/>.
    /// </summary>
    public static class JsonSerializerExtensions
    {
        /// <summary>
        /// Serializes the provided object to JSON. Properties with a <c>null</c> value are ignored.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>The JSON representation of the object.</returns>
        public static string Serialize<T>(this T data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

        /// <summary>
        /// Serializes the provided object to JSON using camelCase property names. Properties with a <c>null</c> value are ignored.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>The JSON representation of the object with camelCase property names.</returns>
        public static string SerializeWithCamelCase<T>(this T data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

#if NETSTANDARD2_1
        /// <summary>
        /// Deserializes the provided camelCase JSON to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The JSON to deserialize.</param>
        /// <returns>The deserialized object, or the default value of <typeparamref name="T"/> when the JSON is empty.</returns>
        [Obsolete("Use DeserializeToObject method")]
        public static T DeserializeFromCamelCase<T>(this string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default!;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Deserializes the provided JSON to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The JSON to deserialize.</param>
        /// <param name="useCamelCase">Boolean indicating whether the JSON uses camelCase property names, default true.</param>
        /// <returns>The deserialized object, or the default value of <typeparamref name="T"/> when the JSON is empty.</returns>
        public static T DeserializeToObject<T>(this string? json, bool useCamelCase = true)
        {
            if (string.IsNullOrWhiteSpace(json)) return default!;

            if (useCamelCase)
            {
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }) ?? throw new InvalidOperationException();
            }

            return JsonSerializer.Deserialize<T>(json) ?? throw new InvalidOperationException();
        }
#endif

#if NET6_0_OR_GREATER
        /// <summary>
        /// Deserializes the provided camelCase JSON to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The JSON to deserialize.</param>
        /// <returns>The deserialized object, or the default value of <typeparamref name="T"/> when the JSON is empty.</returns>
        [Obsolete("Use DeserializeToObject method")]
        public static T? DeserializeFromCamelCase<T>(this string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        /// <summary>
        /// Deserializes the provided JSON to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="json">The JSON to deserialize.</param>
        /// <param name="useCamelCase">Boolean indicating whether the JSON uses camelCase property names, default true.</param>
        /// <returns>The deserialized object, or the default value of <typeparamref name="T"/> when the JSON is empty.</returns>
        public static T? DeserializeToObject<T>(this string? json, bool useCamelCase = true)
        {
            if (string.IsNullOrWhiteSpace(json)) return default;

            if (useCamelCase)
            {
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            return JsonSerializer.Deserialize<T>(json);
        }
#endif
    }
}
