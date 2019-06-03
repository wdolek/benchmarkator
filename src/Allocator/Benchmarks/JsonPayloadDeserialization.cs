using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Allocator.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;

namespace Allocator.Benchmarks
{
    [MemoryDiagnoser]
    [StopOnFirstError]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class JsonPayloadDeserialization
    {
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();
        private HttpResponseMessage _response;

        #region Setup & Cleanup

        [IterationSetup(Targets = new[] { nameof(DeserializeLargeStream), nameof(DeserializeLargeString) })]
        public void HugeGlobalSetup()
        {
            _response = InitFromResource("Allocator.Data.L.json");
        }

        [IterationSetup(Targets = new[] { nameof(DeserializeMediumStream), nameof(DeserializeMediumString) })]
        public void MediumGlobalSetup()
        {
            _response = InitFromResource("Allocator.Data.M.json");
        }

        [IterationSetup(Targets = new[] { nameof(DeserializeSmallStream), nameof(DeserializeSmallString) })]
        public void SmallGlobalSetup()
        {
            _response = InitFromResource("Allocator.Data.S.json");
        }

        [IterationCleanup]
        public void MediumGlobalCleanup()
        {
            _response?.Dispose();
        }

        #endregion

        #region Large

        [Benchmark(Description = "L: stream")]
        public async Task DeserializeLargeStream()
        {
            using (var streamReader = new StreamReader(await _response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                _serializer.Deserialize<MediumData[]>(jsonReader);
            }
        }

        [Benchmark(Description = "L: string")]
        public async Task DeserializeLargeString()
        {
            var content = await _response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<MediumData[]>(content);
        }

        #endregion

        #region Medium

        [Benchmark(Description = "M: stream")]
        public async Task DeserializeMediumStream()
        {
            using (var streamReader = new StreamReader(await _response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                _serializer.Deserialize<MediumData>(jsonReader);
            }
        }

        [Benchmark(Description = "M: string")]
        public async Task DeserializeMediumString()
        {
            var content = await _response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<MediumData>(content);
        }

        #endregion

        #region Small

        [Benchmark(Description = "S: stream")]
        public async Task DeserializeSmallStream()
        {
            using (var streamReader = new StreamReader(await _response.Content.ReadAsStreamAsync()))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                _serializer.Deserialize<SmallData>(jsonReader);
            }
        }

        [Benchmark(Description = "S: string")]
        public async Task DeserializeSmallString()
        {
            var content = await _response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<SmallData>(content);
        }

        #endregion

        public HttpResponseMessage InitFromResource(string resourceName)
        {
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (var streamReader = new StreamReader(resourceStream))
            {
                // nah. we don't really care how it was initialized
                var content = new StringContent(streamReader.ReadToEnd());
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = content
                };
            }
        }
    }
}
