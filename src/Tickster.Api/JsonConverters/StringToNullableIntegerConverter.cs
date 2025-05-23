﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tickster.Api.JsonConverters;
internal class StringToNullableIntegerConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();

            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (int.TryParse(str, out int value))
            {
                return value;
            }
        }

        return reader.GetInt32();
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}

