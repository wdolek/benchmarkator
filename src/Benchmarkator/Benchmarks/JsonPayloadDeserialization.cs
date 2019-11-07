using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Benchmarkator.Data;
using Benchmarkator.Source;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [GenericTypeArguments(typeof(SmallData))]
    [GenericTypeArguments(typeof(MediumData))]
    [GenericTypeArguments(typeof(MediumData[]))]
    public class JsonPayloadDeserialization<T>
    {
        private static readonly Dictionary<Type, string> ResourceMapping = new Dictionary<Type, string>
        {
            [typeof(SmallData)] = "Benchmarkator.Data.S.json",
            [typeof(MediumData)] = "Benchmarkator.Data.M.json",
            [typeof(MediumData[])] = "Benchmarkator.Data.L.json",
        };

        private readonly JsonDeserializator _deserializator = new JsonDeserializator();

        private MemoryStream _memory;

        [GlobalSetup]
        public void Setup()
        {
            var resourceName = ResourceMapping[typeof(T)];
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                _memory = new MemoryStream();
                resourceStream.CopyTo(_memory);
            }
        }

        [Benchmark(Description = "Stream d13n")]
        [Arguments(128)]
        [Arguments(512)]
        [Arguments(1024)]
        [Arguments(4096)]
        public async Task DeserializeLargeStream(int bufferSize)
        {
            await _deserializator.DeserializeFromStream<T>(BuildResponse(_memory), bufferSize);
        }

        [Benchmark(Description = "String d13n")]
        public async Task DeserializeLargeString()
        {
            // internally uses default buffer size (1024)
            await _deserializator.DeserializeFromString<T>(BuildResponse(_memory));
        }

        private static HttpResponseMessage BuildResponse(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
        }
    }
}
