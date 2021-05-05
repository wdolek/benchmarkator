#nullable enable

using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace Benchmarkator.Assemblinator
{
    public class GetAssemblyVersion
    {
        private bool isRead;
        private string? _version;
        
        [GlobalSetup]
        public void Setup()
        {
            isRead = false;
            _version = null;
        }

        [Benchmark(Description = "Always read assembly version by looking for custom assembly attribute")]
        public string? ReadVersion() =>
            Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        
        [Benchmark(Description = "Read assembly version once, store value into field")]
        public string? ReadCachedVersion()
        {
            if (_version is null)
            {
                _version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            }

            return _version;
        }
    }
}