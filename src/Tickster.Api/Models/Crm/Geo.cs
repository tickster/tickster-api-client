using System.Text.Json.Serialization;
using Tickster.Api.JsonConverters;

namespace Tickster.Api.Models.Crm;

// FIXME: Perhaps this is shared with other entities?
public class Geo
{
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Longitude { get; set; }
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal Latitude { get; set; }
    // According to spec, the type is set to "Decimal", but specified as float in JSON. What is correct?
}
