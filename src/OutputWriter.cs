namespace OneBillionRowChallenge;
using System.IO;
using System;

public class OutputWriter : IDisposable
{
    private StreamWriter writer;
    private readonly IEnumerable<KeyValuePair<string, Statistics>> statistics;

    public OutputWriter(string file, IEnumerable<KeyValuePair<string, Statistics>> statistics)
    {
        writer = new StreamWriter(file);
        this.statistics = statistics;
    }

    public void WriteOutput()
    {
        foreach (var keyValuePair in statistics)
        {
            writer.WriteLine("Station statistics : {0}",keyValuePair.Key);
            writer.WriteLine("Min : {0}",keyValuePair.Value.Min);
            writer.WriteLine("Mean : {0}",keyValuePair.Value.Mean());
            writer.WriteLine("Max : {0}",keyValuePair.Value.Max);
        }
    }

    public void Dispose()
    {
        writer.Dispose();
    }
}