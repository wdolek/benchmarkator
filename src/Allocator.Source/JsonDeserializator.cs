using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Allocator.Source
{
    public class JsonDeserializator
    {
        private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();

        public async Task<T> DeserializeFromStream<T>(HttpResponseMessage response, int bufferSize)
        {
            using (var streamReader = BuildNonClosingStreamReader(
                await response.Content.ReadAsStreamAsync(),
                bufferSize))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return _serializer.Deserialize<T>(jsonReader);
            }
        }

        public async Task<T> DeserializeFromString<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            // NB! uses `JsonTextReader` with `StringReader` internally:
            // https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/JsonConvert.cs#L816
            return JsonConvert.DeserializeObject<T>(content);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static StreamReader BuildNonClosingStreamReader(Stream inputStream, int bufferSize) =>
            // default `StreamReader` except `leaveOpen` set to not close input stream - we are reusing it in benchmark
            new StreamReader(
                stream: inputStream,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: bufferSize,
                leaveOpen: true);
    }
}
