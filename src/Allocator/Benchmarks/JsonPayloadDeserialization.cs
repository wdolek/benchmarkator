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
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();

        private readonly Dictionary<Type, string> _resourceMapping = new Dictionary<Type, string>
        {
            [typeof(SmallData)] = "Allocator.Data.S.json",
            [typeof(MediumData)] = "Allocator.Data.M.json",
            [typeof(MediumData[])] = "Allocator.Data.L.json",
        };

        private readonly Dictionary<Type, int> _repeatMapping = new Dictionary<Type, int>
        {
            [typeof(SmallData)] = 10000,
            [typeof(MediumData)] = 1000,
            [typeof(MediumData[])] = 100,
        };

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
        public async Task DeserializeLargeStream()
        {
            for (var i = 0; i < _iterationRepeats; i++)
            {
                var response = BuildResponse(_memory);

                using (var streamReader = BuildNonClosingStreamReader(await response.Content.ReadAsStreamAsync()))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    _serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        [Benchmark(Description = "String d13n")]
        public async Task DeserializeLargeString()
        {
            for (var i = 0; i < _iterationRepeats; i++)
            {
                var response = BuildResponse(_memory);

                var content = await response.Content.ReadAsStringAsync();
                JsonConvert.DeserializeObject<T>(content);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static StreamReader BuildNonClosingStreamReader(Stream inputStream) =>
            new StreamReader(
                stream: inputStream,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: true);
    }
}
