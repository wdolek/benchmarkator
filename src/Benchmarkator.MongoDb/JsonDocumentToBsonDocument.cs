using Benchmarkator.Generator;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Benchmarkator.MongoDb
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class JsonDocumentToBsonDocument
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

        [Benchmark(Baseline = true, Description = "JsonDocument -> string -> BsonDocument")]
        public BsonDocument SerializeJsonString()
        {
            var str = JsonSerializer.Serialize(_jsonDocument);
            return BsonDocument.Parse(str);
        }

        [Benchmark(Description = "JsonDocument: WriteTo -> MemoryStream -> string -> BsonDocument")]
        public BsonDocument SerializeStringFromMemoryStream()
        {
            using var memory = new MemoryStream();
            using (var writer = new Utf8JsonWriter(memory))
            {
                _jsonDocument.WriteTo(writer);
            }

            var str = Encoding.UTF8.GetString(memory.ToArray());
            return BsonDocument.Parse(str);
        }

        [Benchmark(Description = "JsonDocument: WriteTo -> MemoryStream -> BsonDocument")]
        public BsonDocument SerializeUsingMemoryStream()
        {
            using var memory = new MemoryStream();
            using (var writer = new Utf8JsonWriter(memory))
            {
                _jsonDocument.WriteTo(writer);
            }

            memory.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(memory);
            using var jsonReader = new JsonReader(reader);

            var context = BsonDeserializationContext.CreateRoot(jsonReader);
            var document = BsonDocumentSerializer.Instance.Deserialize(context);

            return document;
        }
    }
}
