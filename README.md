## Benchmarkator: benchmarks and stuff

This project is playground for various benchmarks. Creating benchmark is much easier than reading IL.
And it is not just IL - performance of code may be affected by other factors too: JIT, CPU caching, GC, ...

If you know answer why benchmark results with particular numbers, feel free to submit an issue or PR with
explanation.

Otherwise only results are listed - interpretation of results is up to the readers themselves.

### List of benchmarks (areas):

- [JSON Deserialization](src/Benchmarkator.Json/Deserialization/JsonPayloadDeserialization.md):
  observing memory allocation by `StreamReader` buffer
- [Bitmap access](src/Benchmarkator/Bitmap/Bitmap.md):
  determining the fastest approach for implementing bitmap
- [Array access](src/Benchmarkator/ArrayIteration/ArrayIteration.md):
  determining the fastest way to access array item (and way of iteration)
- [Division by `n`](src/Benchmarkator/Division/DivisibleByTwo.md):
  comparing modulo with logical and when dividing by even number
- [Collection Contains ...](src/Benchmarkator.Collections/Contains/CollectionContains.md):
  comparing `corefx` immutable collections with collections from `LanguageExt.Core`

### Running benchmarks

```
dotnet run -c:Release -p src/Benchmarkator -f netcoreapp3.0
```

```
dotnet run -c:Release -p src/Benchmarkator -f netcoreapp3.0 --filter System.Collections*
```