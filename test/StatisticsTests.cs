namespace OneBillionRowChallenge.Tests;

public class StatisticsTests
{
    [Fact]
    public void ChecksStatisticsOfStation()
    {
        Statistics statistics = new Statistics(30);
        Assert.Equal(30, statistics.Min);
    }
    
    [Fact]
    public void UpdateMinimumStatisticsOfStation()
    {
        Statistics statistics = new Statistics(30);
        statistics.Update(25);
        Assert.Equal(25, statistics.Min);
        Assert.Equal(2, statistics.Count);
    }
    [Fact]
    public void UpdateMaximumStatisticsOfStation()
    {
        Statistics statistics = new Statistics(30);
        statistics.Update(40);
        Assert.Equal(40, statistics.Max);
        Assert.Equal(2, statistics.Count);
    }
    
    [Fact]
    public void GetMeanOfAStation()
    {
        Statistics statistics = new Statistics(30);
        Assert.Equal(30, statistics.Mean());
    }
    
    [Fact]
    public void GetMeanOfTwoStation()
    {
        Statistics statistics = new Statistics(30);
        statistics.Update(40);
        Assert.Equal(35, statistics.Mean());
    }
    
    [Fact]
    public void UpdateSumOfStatisticsOfStation()
    {
        Statistics statistics = new Statistics(30);
        statistics.Update(40);

        Assert.Equal(70, statistics.Sum);
    }
}