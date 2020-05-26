using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sdf.Fundamentals.Serializer
{
    public class TextJsonSerializer : ISerializer
    {
        public JsonSerializerOptions JsonSerializerOptions { get; set; }
        public object Deserialize(string json)
        {
           return JsonSerializer.Deserialize(json,typeof(object), JsonSerializerOptions);
        }

        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        }

        public string Serialize(object value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
