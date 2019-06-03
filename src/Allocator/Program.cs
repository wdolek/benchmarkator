using BenchmarkDotNet.Running;

namespace Allocator
{
    class Program
    {
        static void Main(string[] args) =>
            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args);
    }
}
