using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Benchmarkator.MongoDb
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class JsonDocumentSerialization
    {
        public enum DataSize
        {
            Small,
            Medium,
            Large,
        }

        private JsonDocument _jsonDocument = null!;

        [ParamsAllValues]
        public DataSize Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            var jsonGenerator = JsonGenerator.Instance;
            var jsonValue = Size switch
            {
                DataSize.Small => jsonGenerator.SmallJson(),
                DataSize.Medium => jsonGenerator.MediumJson(),
                DataSize.Large => jsonGenerator.LargeJson(),
                _ => throw new NotSupportedException()
            };

            _jsonDocument = JsonDocument.Parse(jsonValue);
        }

        [Benchmark(Baseline = true)]
        public string Serializer()
        {
            return JsonSerializer.Serialize(_jsonDocument);
        }

        [Benchmark]
        public string WriteTo()
        {
            using var memory = new MemoryStream();
            using (var writer = new Utf8JsonWriter(memory))
            {
                _jsonDocument.WriteTo(writer);
            }

            return Encoding.UTF8.GetString(memory.ToArray());
        }
    }
}
