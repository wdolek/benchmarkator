using Newtonsoft.Json;

namespace Benchmarkator.Data
{
    public class SmallData
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Guid { get; set; }
    }
}
