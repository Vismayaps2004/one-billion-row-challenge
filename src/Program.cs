using System.Diagnostics;

namespace  OneBillionRowChallenge;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        long before = GC.GetTotalAllocatedBytes(true);
        Console.WriteLine("=== 1BRC === ");
        if (args.Length == 0)
        {
            Console.WriteLine("provide file path");
            return ;
        }
        
        int gen0Before = GC.CollectionCount(0);
        int gen1Before = GC.CollectionCount(1);
        int gen2Before = GC.CollectionCount(2);
        
        WeatherProcessor weatherProcessor = new WeatherProcessor();
        using WeatherRecordReader reader = new WeatherRecordReader($"../data/{args[0]}");
        
        Stopwatch stopwatch = Stopwatch.StartNew();
        while (reader.TryReadRecord(out ReadOnlySpan<byte> record))
        {
            weatherProcessor.Process(record);
        }

        stopwatch.Stop();
        long after = GC.GetTotalAllocatedBytes(true);

        Console.WriteLine($"Allocated: {after - before:N0} bytes");
        Console.WriteLine($"Gen0: {GC.CollectionCount(0) - gen0Before}");
        Console.WriteLine($"Gen1: {GC.CollectionCount(1) - gen1Before}");
        Console.WriteLine($"Gen2: {GC.CollectionCount(2) - gen2Before}");
        
        IEnumerable<KeyValuePair<int, Statistics>> statistics = weatherProcessor.GetStatistics();
        using OutputWriter outputWriter = new OutputWriter($"../output/{args[0]}", statistics);
        outputWriter.WriteOutput();
        
        Console.WriteLine($"Time taken to process data {stopwatch.Elapsed}");
    }
}