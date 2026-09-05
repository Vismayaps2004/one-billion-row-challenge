namespace OneBillionRowChallenge;

public class WeatherProcessor
{

    private readonly Dictionary<string, Statistics> stationStatistics = new();
    public void Process(string weatherRecord)
    {
        var separatorIndex = weatherRecord.IndexOf(';');
        var station = weatherRecord.Substring(0, separatorIndex);
        var temperature = double.Parse(weatherRecord.AsSpan().Slice(separatorIndex + 1));
        
        if (stationStatistics.TryGetValue(station, out Statistics statistic)) 
        { 
             statistic.Update(temperature);
             return;
        }
        
        Statistics statistics = new Statistics(temperature); 
        stationStatistics.Add(station, statistics);
    }
    
    public IEnumerable<KeyValuePair<string, Statistics>> GetStatistics()
    {
        return stationStatistics;
    }
}