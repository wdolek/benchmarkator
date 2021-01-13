## Benchmarkator: benchmarks and stuff

This project is playground for various benchmarks. Creating benchmark is much easier than reading IL and 
_guessing_ performance characteristics. Be aware, that IL is just one part of puzzle, keep in mind there's
JITter as well and other factors, such as CPU caching, GC, ...

If you know answer why benchmark results with particular numbers, feel free to submit an issue or PR with
explanation.

Only results are listed - interpretation of results is up to the readers themselves.

### List of benchmarks:

- [JSON Deserialization](src/Benchmarkator.Json/Deserialization/JsonPayloadDeserialization.md):
  observing memory allocation by `StreamReader` buffer
- [Bitmap access](src/Benchmarkator/Bitmap/Bitmap.md):
  determining the fastest approach for implementing bitmap
- [Array access](src/Benchmarkator.Collections/Iteration/ArrayIteration.md):
  determining the fastest way to access array item (and way of iteration)
- [Division by `n`](src/Benchmarkator/Division/DivisibleByTwo.md):
  comparing modulo with logical and when dividing by even number
- [Collection Contains ...](src/Benchmarkator.Collections/Contains/ImmutableCollectionContains.md):
  comparing `corefx` immutable collections with collections from `LanguageExt.Core`
- [Collection Create](src/Benchmarkator.Collections/Create/CreateCtor.md):
  comparing `corefx` immutable collections instantiation/creation with `LanguageExt.Core` (`ctor`, `.Create`)
- [Collection Lookup](src/Benchmarkator.Collections/Lookup/ValueLookup.md):
  benchmark of lookup of structured value (e.g. `Id`), comparing array, `List<T>` and `Dictionary<TKey, TValue>`

Not all benchmarks may be listed, check source directly.

### Running benchmarks

```
dotnet run -c Release -p src/Benchmarkator -f net5.0
```

Runnint benchmarks related to `System.Collections`:

```
dotnet run -c Release -p src/Benchmarkator -f net5.0 --filter System.Collections*
```

More about running benchmarks: [BenchmarkDotNet | How to use console arguments](https://benchmarkdotnet.org/articles/guides/console-args.html).
