using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Allocator.Data;
using Allocator.Source;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;

namespace Allocator.Benchmarks
{
    [SimpleJob(RunStrategy.ColdStart)]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [GenericTypeArguments(typeof(SmallData))]
    [GenericTypeArguments(typeof(MediumData))]
    [GenericTypeArguments(typeof(MediumData[]))]
    public class JsonPayloadDeserialization<T>
    {
        private static readonly Dictionary<Type, string> _resourceMapping = new Dictionary<Type, string>
        {
            [typeof(SmallData)] = "Allocator.Data.S.json",
            [typeof(MediumData)] = "Allocator.Data.M.json",
            [typeof(MediumData[])] = "Allocator.Data.L.json",
        };

        private static readonly Dictionary<Type, int> _repeatMapping = new Dictionary<Type, int>
        {
            [typeof(SmallData)] = 10000,
            [typeof(MediumData)] = 1000,
            [typeof(MediumData[])] = 100,
        };

        private readonly JsonDeserializator _deserializator = new JsonDeserializator();

        private MemoryStream _memory;
        private int _iterationRepeats;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var resourceName = _resourceMapping[typeof(T)];
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                _memory = new MemoryStream();
                resourceStream.CopyTo(_memory);
            }

            _iterationRepeats = _repeatMapping[typeof(T)];
        }

        [Benchmark(Description = "Stream d13n")]
        [Arguments(128)]
        [Arguments(512)]
        [Arguments(1024)]
        [Arguments(4096)]
        public async Task DeserializeLargeStream(int bufferSize)
        {
            for (var i = 0; i < _iterationRepeats; i++)
            {
                await _deserializator.DeserializeFromStream<T>(BuildResponse(_memory), bufferSize);
            }
        }

        [Benchmark(Description = "String d13n")]
        public async Task DeserializeLargeString()
        {
            for (var i = 0; i < _iterationRepeats; i++)
            {
                await _deserializator.DeserializeFromString<T>(BuildResponse(_memory));
            }
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
