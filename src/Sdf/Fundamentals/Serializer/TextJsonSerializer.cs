using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sdf.Fundamentals.Serializer
{
    public class TextJsonSerializer : ISerializer
    {
        public TextJsonSerializer()
        { 
        
        }
        public JsonSerializerOptions JsonSerializerOptions { get; set; }
        
        public async Task<object> DeserializeAsync(string json)
        {
            return await JsonSerializer.DeserializeAsync(new MemoryStream(Encoding.UTF8.GetBytes(json)), typeof(object), JsonSerializerOptions);
        }

        public async Task<T> DeserializeAsync<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            return await JsonSerializer.DeserializeAsync<T>(new MemoryStream(Encoding.UTF8.GetBytes(json)), JsonSerializerOptions);
        }

        public async Task<string> SerializeAsync(object value)
        {
            using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, JsonSerializerOptions);
            return Encoding.UTF8.GetString(stream.ToArray());

        }
    }
    sealed class JsonNonStringKeyDictionaryConverter<TKey, TValue> : JsonConverter<IDictionary<TKey, TValue>>
    {
        public override IDictionary<TKey, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var convertedType = typeof(Dictionary<,>)
                .MakeGenericType(typeof(string), typeToConvert.GenericTypeArguments[1]);
            var value = JsonSerializer.Deserialize(ref reader, convertedType, options);
            var instance = (Dictionary<TKey, TValue>)Activator.CreateInstance(
                typeToConvert,
                BindingFlags.Instance | BindingFlags.Public,
                null,
                null,
                CultureInfo.CurrentCulture);
            var enumerator = (IEnumerator)convertedType.GetMethod("GetEnumerator")!.Invoke(value, null);
            var parse = typeof(TKey).GetMethod("Parse", 0, BindingFlags.Public | BindingFlags.Static, null, CallingConventions.Any, new[] { typeof(string) }, null);
            if (parse == null) throw new NotSupportedException($"{typeof(TKey)} as TKey in IDictionary<TKey, TValue> is not supported.");
            while (enumerator.MoveNext())
            {
                var element = (KeyValuePair<string?, TValue>)enumerator.Current;
                instance.Add((TKey)parse.Invoke(null, new[] { element.Key }), element.Value);
            }
            return instance;
        }

        public override void Write(Utf8JsonWriter writer, IDictionary<TKey, TValue> value, JsonSerializerOptions options)
        {
            var convertedDictionary = new Dictionary<string?, TValue>(value.Count);
            foreach (var (k, v) in value) convertedDictionary[k?.ToString()] = v;
            JsonSerializer.Serialize(writer, convertedDictionary, options);
            convertedDictionary.Clear();
        }
    }

    sealed class JsonNonStringKeyDictionaryConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType) return false;
            if (typeToConvert.GenericTypeArguments[0] == typeof(string)) return false;
            return typeToConvert.GetInterface("IDictionary") != null;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(JsonNonStringKeyDictionaryConverter<,>)
                .MakeGenericType(typeToConvert.GenericTypeArguments[0], typeToConvert.GenericTypeArguments[1]);
            var converter = (JsonConverter)Activator.CreateInstance(
                converterType,
                BindingFlags.Instance | BindingFlags.Public,
                null,
                null,
                CultureInfo.CurrentCulture);
            return converter;
        }
    }
}
