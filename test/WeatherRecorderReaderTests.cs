namespace OneBillionRowChallenge.Tests;
using Xunit;

public class WeatherRecorderReaderTests
{
    [Fact]
    public void ShouldReadFirstLineFromFile()
    {
        WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-1.txt");
        Assert.Equal("Tokyo;35.6897", weatherRecordReader.ReadLine());
    }
    
    [Fact]
    public void ShouldReadSecondLineFromFile()
    {
        WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-2.txt");
        weatherRecordReader.ReadLine();
        Assert.Equal("Paris;40", weatherRecordReader.ReadLine());
    }
    
    [Fact]
    public void ShouldReadNullFromFile()
    {
        WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-1.txt");
        weatherRecordReader.ReadLine();
        Assert.Null(weatherRecordReader.ReadLine());
    }
    
    [Fact]
    public void ShouldNotReadfAfterDispose()
    {
        WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-1.txt");
        weatherRecordReader.Dispose();
        Assert.Throws<ObjectDisposedException>(() => weatherRecordReader.ReadLine());
    }
}
