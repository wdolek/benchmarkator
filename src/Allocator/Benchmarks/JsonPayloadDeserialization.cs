using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Allocator.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;

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
        private const int RepeatsWithinIteration = 100;

        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();
        private readonly Dictionary<Type, string> _resourceMapping = new Dictionary<Type, string>
        {
            [typeof(SmallData)] = "Allocator.Data.S.json",
            [typeof(MediumData)] = "Allocator.Data.M.json",
            [typeof(MediumData[])] = "Allocator.Data.L.json",
        };

        private MemoryStream _memory;
        private HttpResponseMessage _response;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var resourceName = _resourceMapping[typeof(T)];
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                _memory = new MemoryStream();
                resourceStream.CopyTo(_memory);
            }

            _response = BuildResponse(_memory);
        }

        [Benchmark(Description = "Stream d13n")]
        public async Task DeserializeLargeStream()
        {
            for (var i = 0; i < RepeatsWithinIteration; i++)
            {
                _memory.Seek(0, SeekOrigin.Begin);

                using (var streamReader = BuildNonClosingStreamReader(await _response.Content.ReadAsStreamAsync()))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    _serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        [Benchmark(Description = "String d13n")]
        public async Task DeserializeLargeString()
        {
            for (var i = 0; i < RepeatsWithinIteration; i++)
            {
                _memory.Seek(0, SeekOrigin.Begin);

                var content = await _response.Content.ReadAsStringAsync();
                JsonConvert.DeserializeObject<T>(content);
            }
        }

        private static HttpResponseMessage BuildResponse(Stream stream)
        {
            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static StreamReader BuildNonClosingStreamReader(Stream inputStream) =>
            new StreamReader(inputStream, Encoding.UTF8, true, 1024, true);
    }
}
