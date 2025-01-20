﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tickster.Api.JsonConverters;

internal class StringToDecimalConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (decimal.TryParse(reader.GetString(), CultureInfo.InvariantCulture, out decimal value))
            {
                return value;
            }
        }

        return reader.GetDecimal();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
