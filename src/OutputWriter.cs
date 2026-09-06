namespace OneBillionRowChallenge;
using System.IO;
using System;

public class OutputWriter(string file, IEnumerable<KeyValuePair<string, Statistics>> statistics)
    : IDisposable
{
    private StreamWriter writer = new(file);

    public void WriteOutput()
    {
        foreach (var keyValuePair in statistics)
        {
            writer.WriteLine("Station statistics : {0}",keyValuePair.Key);
            writer.WriteLine("Min : {0}",keyValuePair.Value.Min / 10);
            writer.WriteLine("Mean : {0}",keyValuePair.Value.Mean()  / 10);
            writer.WriteLine("Max : {0}",keyValuePair.Value.Max / 10);
        }
    }

    public void Dispose()
    {
        writer.Dispose();
    }
}