using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Benchmarkator.Json
{
    internal class SystemTextJsonDeserializator : IJsonDeserializator
    {
        public async Task<T> DeserializeFromStream<T>(HttpResponseMessage response, int bufferSize)
        {
            var responsePayloadStream = await response.Content.ReadAsStreamAsync();
            var serializerOptions = new JsonSerializerOptions { DefaultBufferSize = bufferSize };

            return await JsonSerializer.DeserializeAsync<T>(responsePayloadStream, serializerOptions) 
                ?? throw new Exception("Deserializer returnet `null`");
        }

        public async Task<T?> DeserializeFromString<T>(HttpResponseMessage response)
        {
            var responsePayloadString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responsePayloadString);
        }
    }
}
