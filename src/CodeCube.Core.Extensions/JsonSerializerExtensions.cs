using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeCube.Core.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static string Serialize<T>(this T data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

        public static string SerializeWithCamelCase<T>(this T data)
        {
            return JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

#if NETSTANDARD2_1
        public static T DeserializeFromCamelCase<T>(this string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? throw new InvalidOperationException();
        }
#endif

#if NET6_0_OR_GREATER
    public static T? DeserializeFromCamelCase<T>(this string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
#endif
    }
}