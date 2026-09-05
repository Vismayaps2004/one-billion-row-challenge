using System.Diagnostics;

namespace  OneBillionRowChallenge;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 1BRC === ");
        if (args.Length == 0)
        {
            Console.WriteLine("provide file path");
            return ;
        }

        Stopwatch stopwatch = Stopwatch.StartNew();
        WeatherProcessor weatherProcessor = new WeatherProcessor();
        using WeatherRecordReader weatherRecordReader = new WeatherRecordReader($"../data/{args[0]}");
        string? weatherRecord = weatherRecordReader.ReadLine();
        while (weatherRecord != null)
        {
            weatherProcessor.Process(weatherRecord);
            weatherRecord = weatherRecordReader.ReadLine();
        }

        stopwatch.Stop();
        IEnumerable<KeyValuePair<string, Statistics>> statistics = weatherProcessor.GetStatistics();
        using OutputWriter outputWriter = new OutputWriter($"../output/{args[0]}", statistics);
        outputWriter.WriteOutput();
        
        Console.WriteLine($"Time taken to process data {stopwatch.Elapsed}");
    }
}