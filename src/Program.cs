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

        WeatherProcessor weatherProcessor = new WeatherProcessor();
        using WeatherRecordReader weatherRecordReader = new WeatherRecordReader(args[0]);
        string? weatherRecord = weatherRecordReader.ReadLine();
        while (weatherRecord != null)
        {
            weatherProcessor.Process(weatherRecord);
            weatherRecord = weatherRecordReader.ReadLine();
        }
    }
}