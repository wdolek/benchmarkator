using Benchmarkator.Collections;
using BenchmarkDotNet.Running;

namespace Benchmarkator
{
    class Program
    {
        static void Main(string[] args) =>
            BenchmarkSwitcher
                .FromAssemblies(new[]
                {
                    typeof(Program).Assembly,
                    typeof(Categories).Assembly
                })
                .Run(args);
    }
}
