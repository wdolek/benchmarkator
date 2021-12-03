using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Benchmarkator.MongoDb
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class JsonToBson
    {
        private JsonDocument _jsonDocument = null!;

        [GlobalSetup]
        public void Setup()
        {
            var dataEmbededResourceName = "Benchmarkator.MongoDb.Data.entity.json";
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dataEmbededResourceName))
            {
                if (resourceStream is null)
                {
                    throw new Exception($"Resource '{dataEmbededResourceName}' not visible/available");
                }

                var memory = new MemoryStream();
                resourceStream.CopyTo(memory);

                memory.Seek(0, SeekOrigin.Begin);

                _jsonDocument = JsonDocument.Parse(memory);
            }
        }

        [Benchmark(Baseline = true, Description = "JsonDocument -> string -> BsonDocument")]
        public BsonDocument SerializeJsonString()
        {
            var str = JsonSerializer.Serialize(_jsonDocument);
            return BsonDocument.Parse(str);
        }

        [Benchmark(Description = "JsonDocument -> MemoryStream -> str -> BsonDocument")]
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

        [Benchmark(Description = "JsonDocument -> MemoryStream -> BsonDocument")]
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
