using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sdf.Fundamentals.Json
{
    
    public class LongJsonConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt64();
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
        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
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
