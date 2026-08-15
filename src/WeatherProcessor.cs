namespace OneBillionRowChallenge;

public class WeatherProcessor
{

    private readonly Dictionary<string, Statistics> stationStatistics = new();
    public void Process(string weatherRecord)
    {
        var weatherData = weatherRecord.Split(';');
        string station = weatherData[0];
        double temperature = double.Parse(weatherData[1]);
        
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