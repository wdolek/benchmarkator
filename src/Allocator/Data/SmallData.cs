using Newtonsoft.Json;

namespace Allocator.Data
{
    class SmallData
    {
        [JsonProperty("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
