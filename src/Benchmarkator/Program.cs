using BenchmarkDotNet.Running;
using System.Reflection;

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
                    Assembly.Load("Benchmarkator.Collections"),

                    // json
                    Assembly.Load("Benchmarkator.Json")
                })
                .Run(args);
    }
}
