using System.Net.Http;
using System.Threading.Tasks;

namespace Benchmarkator.Json;

internal interface IJsonDeserializator
{
    Task<T> DeserializeFromStream<T>(HttpResponseMessage response, int bufferSize);
    Task<T?> DeserializeFromString<T>(HttpResponseMessage response);
}
