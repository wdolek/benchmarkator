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
                    typeof(Program).Assembly,
                    Assembly.Load("Benchmarkator.Collections"),
                    Assembly.Load("Benchmarkator.Json"),
                    Assembly.Load("Benchmarkator.MongoDb")
                })
                .Run(args);
    }
}
