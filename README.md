## Benchmarkator: benchmarks and stuff

This project is playground for various benchmarks. Creating benchmark is much easier than reading IL and 
_guessing_ performance characteristics. Be aware, that IL is just one part of puzzle, keep in mind there's
JITter as well and other factors, such as CPU caching, GC, ...

If you know answer why benchmark results with particular numbers, feel free to submit an issue or PR with
explanation.

Only results are listed - interpretation of results is up to the readers themselves.

### List of benchmarks:

Not all benchmarks may be listed, check source directly.

- [Get Assembly Version](src/Benchmarkator/Assemblinator/GetAssemblyVersion.md):
  demonstrating how impactful it is to get custom assembly attribute over and over
- [Bitmap access](src/Benchmarkator/Bitmap/Bitmap.md):
  determining the fastest approach for implementing bitmap
- [Division by `n`](src/Benchmarkator/Division/DivisibleByTwo.md):
  comparing modulo with logical and when dividing by even number
- [Formatting string while rendering just part of it](src/Benchmarkator/Stringator/StringFormatSubstring.md):
  comparing approaches to format string in combination of `string.Substring`

#### JSON

- [JSON Deserialization](src/Benchmarkator.Json/Deserialization/JsonPayloadDeserialization.md):
  observing memory allocation by `StreamReader` buffer

#### Collection benchmarks

- [Array access](src/Benchmarkator.Collections/Iteration/ArrayIteration.md):
  determining the fastest way to access array item (and way of iteration)
- [Collection Contains ...](src/Benchmarkator.Collections/Contains/ImmutableCollectionContains.md):
  comparing `corefx` immutable collections with collections from `LanguageExt.Core`
- [Collection Create](src/Benchmarkator.Collections/Create/CreateCtor.md):
  comparing `corefx` immutable collections instantiation/creation with `LanguageExt.Core` (`ctor`, `.Create`)
- [Collection Lookup](src/Benchmarkator.Collections/Lookup/ValueLookup.md):
  benchmark of lookup of structured value (e.g. `Id`), comparing array, `List<T>` and `Dictionary<TKey, TValue>`
- [`.ToArray` vs `.ToList`](src/Benchmarkator.Collections/ToCollection/ToCollection.md):
  bemchmark comparing performance of `.ToArray()` and `.ToList()`

#### MongoDB

- [System.Text.Json.JsonDocument serialization](src/Benchmarkator.MongoDb/JsonDocumentSerialization.md)
  benchmark of serialization of `System.Text.Json.JsonDocument`
- [System.Text.Json.JsonDocument to MongoDB.Bson.BsonDocument](src/Benchmarkator.MongoDb/JsonDocumentToBsonDocument.md)
  benchmark of conversion of JSON to BSON document

### Running benchmarks

```
dotnet run -c Release --project src/Benchmarkator -f net6.0
```

Runnint benchmarks related to `System.Collections`:

```
dotnet run -c Release --project src/Benchmarkator -f net6.0 --filter System.Collections*
```

More about running benchmarks: [BenchmarkDotNet | How to use console arguments](https://benchmarkdotnet.org/articles/guides/console-args.html).
