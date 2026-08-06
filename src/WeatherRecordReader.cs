namespace OneBillionRowChallenge;
using System.IO;

public class WeatherRecordReader : IDisposable
{
    private readonly StreamReader reader;

    public WeatherRecordReader(string filePath)
    {
        reader = new StreamReader(filePath);
    }

    public string?  ReadLine()
    {
        return reader.ReadLine();
    }

    public void Dispose()
    {
        reader.Dispose();
    }
}