using Benchmarkator.Json;
using BenchmarkDotNet.Running;
using System.Collections;

namespace Benchmarkator
{
    class Program
    {
        static void Main(string[] args) =>
            BenchmarkSwitcher
                .FromAssemblies(new[]
                {
                    // main assembly
                    typeof(Program).Assembly,

                    // collections
                    typeof(ContainsFalse<>).Assembly,

                    // json
                    typeof(JsonPayloadDeserialization<>).Assembly
                })
                .Run(args);
    }
}
