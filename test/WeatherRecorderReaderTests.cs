namespace OneBillionRowChallenge.Tests;
using Xunit;

public class WeatherRecorderReaderTests
{
    [Fact]
    public void ShouldReadFirstLineFromFile()
    {
        using WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-1.txt");
        Assert.Equal("Tokyo;35.6897", weatherRecordReader.ReadLine());
    }
    
    [Fact]
    public void ShouldReadSecondLineFromFile()
    {
        using WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-2.txt");
        weatherRecordReader.ReadLine();
        Assert.Equal("Paris;40", weatherRecordReader.ReadLine());
    }
    
    [Fact]
    public void ShouldReadNullFromFile()
    {
        using WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-1.txt");
        weatherRecordReader.ReadLine();
        Assert.Null(weatherRecordReader.ReadLine());
    }
    
    [Fact]
    public void ShouldNotReadAfterDispose()
    {
        using WeatherRecordReader weatherRecordReader = new WeatherRecordReader("data/measurements-1.txt");
        Assert.Throws<ObjectDisposedException>(() => weatherRecordReader.ReadLine());
    }
}
