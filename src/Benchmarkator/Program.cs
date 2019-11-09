using Benchmarkator.Collections;
using Benchmarkator.Json;
using BenchmarkDotNet.Running;

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
                    typeof(ValuesGenerator).Assembly,

                    // json
                    typeof(JsonPayloadDeserialization<>).Assembly
                })
                .Run(args);
    }
}
