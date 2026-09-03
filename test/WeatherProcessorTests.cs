namespace OneBillionRowChallenge.Tests;

public class WeatherProcessorTests
{
    [Fact]
    public void Process_WhenFileContainsOneRecord_ShouldCreateStationStatistics()
    {
        WeatherProcessor weatherProcessor = new WeatherProcessor();
        weatherProcessor.Process("Tokyo;35.6897");
        IEnumerable<KeyValuePair<string, Statistics>> statisticsMap = weatherProcessor.GetStatistics();
        Assert.Single(statisticsMap);
        Statistics tokyo = statisticsMap.Single().Value;
        Assert.Equal(1, tokyo.Count);
        
        Assert.Equal(35.6897, tokyo.Min);
        Assert.Equal(35.6897, tokyo.Max);
        Assert.Equal(35.6897, tokyo.Sum);
        Assert.Equal(1, tokyo.Count);
    }
    
    [Fact]
    public void Process_WhenFileContainsMultipleRecord_ShouldCreateStationStatistics()
    {
        WeatherProcessor weatherProcessor = new WeatherProcessor();
       weatherProcessor.Process("Tokyo;35.6897");
        weatherProcessor.Process("Paris;40");
        var statistics = weatherProcessor.GetStatistics();
        Statistics tokyo = statistics.Single(x => x.Key == "Tokyo").Value;
        Statistics paris = statistics.Single(x => x.Key == "Paris").Value;
        
        Assert.Equal(2, statistics.Count());
        Assert.Equal(35.6897, tokyo.Min);
        Assert.Equal(35.6897, tokyo.Max);
        Assert.Equal(35.6897, tokyo.Sum);
        Assert.Equal(1, tokyo.Count);
        Assert.Equal(40, paris.Min);
        Assert.Equal(40, paris.Max);
        Assert.Equal(1, paris.Count);
        Assert.Equal(40, paris.Sum);
    }
    
    [Fact]
    public void Process_WhenFileContainsMultipleRecordWithSameStation_ShouldUpdateStationStatistics()
    {
        WeatherProcessor weatherProcessor = new WeatherProcessor(); 
        weatherProcessor.Process("Tokyo;35.6897");
        weatherProcessor.Process("Tokyo;30");
        weatherProcessor.Process("Paris;40");
        var statistics = weatherProcessor.GetStatistics();
        
        Statistics tokyo = statistics.Single(x => x.Key == "Tokyo").Value;
        Statistics paris = statistics.Single(x => x.Key == "Paris").Value;

        Assert.Equal(2, statistics.Count());
        Assert.Equal(30, tokyo.Min);
        Assert.Equal(35.6897, tokyo.Max);
        Assert.Equal(65.6897, tokyo.Sum);
        Assert.Equal(2, tokyo.Count);
        Assert.Equal(32.84485, tokyo.Mean(), 5);
        Assert.Equal(40, paris.Min);
        Assert.Equal(40, paris.Max);
        Assert.Equal(40, paris.Sum);
        Assert.Equal(1, paris.Count);
    }
}
