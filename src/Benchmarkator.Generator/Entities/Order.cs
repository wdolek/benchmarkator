using System.Text.Json.Serialization;

namespace Benchmarkator.Generator.Entities
{
    public class Order
    {
        [JsonPropertyName("id")]
        public int OrderId { get; init; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; init; } = null!;

        [JsonPropertyName("ordered_item")]
        public string Item { get; init; } = null!;

        [JsonPropertyName("quantity")]
        public int Quantity { get; init; }
    }
}
