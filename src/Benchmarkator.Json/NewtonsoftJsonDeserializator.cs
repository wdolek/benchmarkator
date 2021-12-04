using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Benchmarkator.Json;

public class NewtonsoftJsonDeserializator : IJsonDeserializator
{
    private readonly JsonSerializer _serializer = JsonSerializer.CreateDefault();

    public async Task<T> DeserializeFromStream<T>(HttpResponseMessage response, int bufferSize)
    {
        var responsePayloadStream = await response.Content.ReadAsStreamAsync();

        using var streamReader = BuildNonClosingStreamReader(responsePayloadStream, bufferSize);
        using var jsonReader = new JsonTextReader(streamReader);

        return _serializer.Deserialize<T>(jsonReader) ?? throw new Exception("Deserializer returnet `null`");
    }

    public async Task<T?> DeserializeFromString<T>(HttpResponseMessage response)
    {
        var responsePayloadString = await response.Content.ReadAsStringAsync();

        // NB! uses `JsonTextReader` with `StringReader` internally:
        // https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/JsonConvert.cs#L816
        return JsonConvert.DeserializeObject<T>(responsePayloadString);
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
