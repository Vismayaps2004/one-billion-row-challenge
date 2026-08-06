namespace OneBillionRowChallenge.Tests;

public class WeatherProcessorTests
{
    [Fact]
    public void Process_WhenFileContainsOneRecord_ShouldCreateStationStatistics()
    {
        WeatherProcessor weatherProcessor = new WeatherProcessor();
        Dictionary<string, Statistics> statisticsMap = weatherProcessor.stationStatistics;
        weatherProcessor.Process("Tokyo;35.6897");
        Assert.Equal(1, statisticsMap.Count);
        bool found = statisticsMap.TryGetValue("Tokyo", out Statistics statistics);

        Assert.True(found);
        Assert.Equal(35.6897, statistics.Min);
        Assert.Equal(35.6897, statistics.Max);
        Assert.Equal(35.6897, statistics.Sum);
        Assert.Equal(1, statistics.Count);
    }
    
    [Fact]
    public void Process_WhenFileContainsMultipleRecord_ShouldCreateStationStatistics()
    {
        WeatherProcessor weatherProcessor = new WeatherProcessor();
       weatherProcessor.Process("Tokyo;35.6897");
        weatherProcessor.Process("Paris;40");
        Assert.Equal(2, weatherProcessor.stationStatistics.Count);
        bool foundTokyo = weatherProcessor.stationStatistics.TryGetValue("Tokyo", out Statistics tokyo);
        bool foundParis = weatherProcessor.stationStatistics.TryGetValue("Paris", out Statistics paris);

        Assert.True(foundTokyo);
        Assert.True(foundParis);
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
        Assert.Equal(2, weatherProcessor.stationStatistics.Count);
        bool foundTokyo = weatherProcessor.stationStatistics.TryGetValue("Tokyo", out Statistics tokyo);
        bool foundParis = weatherProcessor.stationStatistics.TryGetValue("Paris", out Statistics paris);

        Assert.True(foundTokyo);
        Assert.True(foundParis);
        Assert.Equal(30, tokyo.Min);
        Assert.Equal(35.6897, tokyo.Max);
        Assert.Equal(65.6897, tokyo.Sum);
        Assert.Equal(2, tokyo.Count);
        Assert.Equal(40, paris.Min);
        Assert.Equal(40, paris.Max);
        Assert.Equal(40, paris.Sum);
        Assert.Equal(1, paris.Count);
    }
}
