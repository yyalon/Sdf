using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sdf.Fundamentals.Json
{
    public class DoubleJsonConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble();
            }
            string longValue = reader.GetString();

            if (string.IsNullOrEmpty(longValue))
            {
                return 0;
            }
            return Convert.ToInt64(longValue);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (value != 0)
            {
                writer.WriteStringValue(value.ToString());
            }
            else
            {
                writer.WriteStringValue(String.Empty);
            }

        }
    }
    public class DoubleAvailableNullJsonConverter : JsonConverter<double?>
    {
        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble();
            }
            string longValue = reader.GetString();

            if (string.IsNullOrEmpty(longValue))
            {
                return null;
            }
            return Convert.ToInt64(longValue);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            if (value != 0)
            {
                writer.WriteStringValue(value.ToString());
            }
            else
            {
                writer.WriteStringValue(String.Empty);
            }

        }
    }
}
