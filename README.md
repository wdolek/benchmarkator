## Benchmarkator: benchmarks and stuff

This project is playground for various benchmarks. Creating benchmark is much easier than reading IL and
_guessing_ performance characteristics. Be aware that IL is just one part of puzzle, keep in mind there's
JITter as well and other factors, such as CPU caching, GC, ...

If you can explain benchmark results/you can link to documentation, feel free to submit an issue or PR with
explanation/link.

Only results are listed - interpretation of results is up to the readers themselves.

### List of benchmarks:

Not all benchmarks may be listed, check source directly.

- [Get Assembly Version](src/Benchmarkator/Assemblinator/GetAssemblyVersion.md):
  demonstrating how impactful it is to get custom attribute over and over
- [Bitmap access](src/Benchmarkator/Bitmap/Bitmap.md):
  determining the fastest approach for implementing bitmap
- [Division by `n`](src/Benchmarkator/Division/DivisibleByTwo.md):
  comparing modulo with logical AND when dividing by even number
- [Using lambdas](src/Benchmarkator/Lambdinator/LambdaUsage.md):
  comparing different ways how to write lambdas/anonymous functions
- [String concatenation](src/Benchmarkator/Stringator/StringConcat.md):
  simple concat of two strings
- [Formatting string while rendering just part of it](src/Benchmarkator/Stringator/StringFormatSubstring.md):
  comparing approaches to format string in combination of `string.Substring`
- [Throw or return](src/Benchmarkator/Exceptions/ThrowOrReturn.md):
  demonstrating price of throwing an exception
- [Reading country code from culture string](src/Benchmarkator/Stringator/CultureStringSlicator.md):
  what's the fastest way to read country code from culture string?

#### Collection benchmarks

- [Add to collection](src/Benchmarkator.Collections/Add/AddToCollection.md):
  comparing different ways to add entry to collection (List/Dictionary with default capacity and with capacity set to `n`)
- [Add to `ConcurrentBag` and `ConcurrentDictionary`](src/Benchmarkator.Collections/Add/RedundantConcurrentCollection.md):
  demonstrating price of adding to concurrent collection when concurrency is not needed (e.g. after refactoring)
- [Add to `ConcurrentBag`](src/Benchmarkator.Collections/Add/AddToConcurrentBag.md):
  demonstrating price of creating `ConcurrentBag<>` with known initial data
- [Collection Contains ...](src/Benchmarkator.Collections/Contains/ImmutableCollectionContains.md):
  comparing `corefx` immutable collections with collections from `LanguageExt.Core`-
- [Collection Create](src/Benchmarkator.Collections/Create/CreateCtor.md):
  comparing `corefx` immutable collections instantiation/creation with `LanguageExt.Core` (`ctor`, `.Create`)
- [Creating empty collection](src/Benchmarkator.Collections/Create/EmptyCollection.md):
  comparison of creating empty `List` and `Dictionary`
- [Array access](src/Benchmarkator.Collections/Iteration/ArrayIteration.md):
  determining the fastest way to access array item (and way of iteration)
- [Array/Array as `IEnumerable`/`List` `foreach`](src/Benchmarkator.Collections/Iteration/ArrayListForeachIteration.md):
  difference between iterating over array and `List` using `foreach`
- [Accessing array/`List<>` value using indexer](src/Benchmarkator.Collections/Iteration/CollectionIterationIndexerAccess.md):
  comparison of accessing value via `[]` on array and `List<>`
- [Deconstructing `KeyValuePair<,>`](src/Benchmarkator.Collections/Iteration/DictionaryDeconstructKvp.md):
  finding difference of using key value pair deconstruction
- [Deconstructing `KeyValuePair<,>` vs accessing value using key](src/Benchmarkator.Collections/Iteration/DictionaryDeconstructOrAccess.md):
  determining price of accessing value using key instead of simple iteration
- [Differences enumerating `IEnumerable`](src/Benchmarkator.Collections/Iteration/EnumerableIteration.md):
  demonstrating difference of `IEnumerable` implementation
- [Enumerating `ImmutableArray` various way](src/Benchmarkator.Collections/Iteration/ImmutableArrayIteration.md):
  finding difference of various enumeration approaches on `ImmutableArray`
- [Collection Lookup](src/Benchmarkator.Collections/Lookup/ValueLookup.md):
  benchmark of lookup of structured value (e.g. `Id`), comparing array, `List<T>` and `Dictionary<TKey, TValue>`
- [Creating array or `List<>` from collection](src/Benchmarkator.Collections/ToCollection/ToCollection.md):
  benchmark comparing performance of `.ToArray()` and `.ToList()`
- [Creating `Dictionary<,>` from collection](src/Benchmarkator.Collections/ToCollection/ToDictionary.md):
  benchmark of creating dictionary out of collection using LINQ and simple implementation

#### JSON

- [JSON Deserialization](src/Benchmarkator.Json/Deserialization/JsonPayloadDeserialization.md):
  observing memory allocation by `StreamReader` buffer

#### MongoDB

- [System.Text.Json.JsonDocument serialization](src/Benchmarkator.MongoDb/JsonDocumentSerialization.md)
  benchmark of serialization of `System.Text.Json.JsonDocument`
- [System.Text.Json.JsonDocument to MongoDB.Bson.BsonDocument](src/Benchmarkator.MongoDb/JsonDocumentToBsonDocument.md)
  benchmark of conversion of JSON to BSON document

### Running benchmarks

```
dotnet run -c Release --project src/Benchmarkator -f net6.0
```

... so running benchmarks related to collections is done using command:

```
dotnet run -c Release --project src/Benchmarkator -f net6.0 --filter Benchmarkator.Collections*
```

More about running benchmarks: [BenchmarkDotNet | How to use console arguments](https://benchmarkdotnet.org/articles/guides/console-args.html).
