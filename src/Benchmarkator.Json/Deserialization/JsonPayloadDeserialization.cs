﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Benchmarkator.Json.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Benchmarkator.Json.Deserialization;

[MemoryDiagnoser]
[CategoriesColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[GenericTypeArguments(typeof(SmallData))]
[GenericTypeArguments(typeof(MediumData))]
[GenericTypeArguments(typeof(MediumData[]))]
public class JsonPayloadDeserialization<T>
{
    private static readonly Dictionary<Type, string> ResourceMapping = new Dictionary<Type, string>
    {
        [typeof(SmallData)] = "Benchmarkator.Json.Data.S.json",
        [typeof(MediumData)] = "Benchmarkator.Json.Data.M.json",
        [typeof(MediumData[])] = "Benchmarkator.Json.Data.L.json",
    };

    private readonly NewtonsoftJsonDeserializator _newtonsoftJsonDeserializator = new NewtonsoftJsonDeserializator();
    private readonly SystemTextJsonDeserializator _systemTextJsonDeserializator = new SystemTextJsonDeserializator();

    private MemoryStream _memory = null!;

    [GlobalSetup]
    public void Setup()
    {
        var resourceName = ResourceMapping[typeof(T)];
        using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            if (resourceStream is null)
            {
                throw new Exception($"Resource '{resourceName}' not visible/available");
            }

            _memory = new MemoryStream();
            resourceStream.CopyTo(_memory);
        }
    }

    [Benchmark(Description = "Stream d13n (Newtonsoft)")]
    [BenchmarkCategory("newtonsoft")]
    [Arguments(128)]
    [Arguments(512)]
    [Arguments(1024)]
    [Arguments(4096)]
    public async Task DeserializeLargeStream(int bufferSize)
    {
        await _newtonsoftJsonDeserializator.DeserializeFromStream<T>(BuildResponse(_memory), bufferSize);
    }

    [Benchmark(Description = "String d13n (Newtonsoft)")]
    [BenchmarkCategory("newtonsoft")]
    public async Task DeserializeLargeString()
    {
        // internally uses default buffer size (1024)
        await _newtonsoftJsonDeserializator.DeserializeFromString<T>(BuildResponse(_memory));
    }

    [Benchmark(Description = "Stream d13n (System.Text.Json)")]
    [BenchmarkCategory("system.text.json")]
    [Arguments(128)]
    [Arguments(512)]
    [Arguments(1024)]
    [Arguments(4096)]
    public async Task DeserializeLargeStreamSTJ(int bufferSize)
    {
        await _systemTextJsonDeserializator.DeserializeFromStream<T>(BuildResponse(_memory), bufferSize);
    }

    [Benchmark(Description = "String d13n (System.Text.Json)")]
    [BenchmarkCategory("system.text.json")]
    public async Task DeserializeLargeStringSTJ()
    {
        // internally uses default buffer size (1024)
        await _systemTextJsonDeserializator.DeserializeFromString<T>(BuildResponse(_memory));
    }

    private static HttpResponseMessage BuildResponse(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);

        var content = new StreamContent(stream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = content
        };
    }
}
