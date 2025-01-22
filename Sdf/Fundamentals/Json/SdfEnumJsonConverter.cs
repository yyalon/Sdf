using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sdf.Fundamentals.Json
{
    public class SdfEnumConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var enumtype = Nullable.GetUnderlyingType(typeToConvert);
                if (enumtype != null && enumtype.IsEnum)
                {
                    return true;
                }
            }
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonConverter = Create(typeToConvert, options);
            return jsonConverter;
        }
        internal static JsonConverter Create(Type typeToConvert, JsonSerializerOptions options)
        {
            var enumtype = typeToConvert.IsEnum ? typeToConvert : Nullable.GetUnderlyingType(typeToConvert);
            var converterType = GetEnumConverterType(enumtype);

            return (JsonConverter)Activator.CreateInstance(
               converterType,
               BindingFlags.Instance | BindingFlags.Public,
               binder: null,
               args: null,
               culture: null)!;
        }

        private static Type GetEnumConverterType(Type enumType)
        {
            return typeof(SdfEnumJsonConverter<>).MakeGenericType(enumType);
        }
    }

    public class SdfEnumJsonConverter<T> : JsonConverter<T?> where T : struct
    {
        public SdfEnumJsonConverter()
        {

        }
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var enumtype = typeToConvert.GenericTypeArguments[0];
                if (enumtype.IsEnum)
                {
                    return true;
                }
            }
            if (typeToConvert.IsEnum)
            {
                return true;
            }
            return false;
        }
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumtype = Nullable.GetUnderlyingType(typeToConvert);
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            if (reader.TokenType == JsonTokenType.String && reader.GetString() == String.Empty)
            {
                return null;
            }
            if (reader.TokenType == JsonTokenType.Number)
            {
                int val = reader.GetInt32();
                return (T)Enum.ToObject(enumtype, val);
            }
            if (reader.TokenType == JsonTokenType.String)
            {
                return (T)Enum.Parse(enumtype, reader.GetString());
            }
            throw new Exception("不支持的类型转换");
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteNumberValue(Convert.ToInt32(value.Value));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
