using Newtonsoft.Json;

namespace Benchmarkator.Json.Data;

public class SmallData
{
    [JsonProperty("_id")]
    public string? Id { get; set; }
    public string? Guid { get; set; }
}
