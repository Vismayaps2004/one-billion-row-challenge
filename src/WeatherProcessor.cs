using OneBillionRowChallenge;

public class WeatherProcessor
{

    private Dictionary<string, Statistics> StationStatistics = new();
    public Dictionary<string, Statistics> Process()
    {
        using var reader = new StreamReader("../data/measurements-1.txt");
        string? weatherRecord = reader.ReadLine();
        while (weatherRecord != null)
        {
            GenerateStatistics(weatherRecord);
            weatherRecord = reader.ReadLine();
        }
        return StationStatistics; 
        
    }

    private void GenerateStatistics(string weatherRecord)
    {
        var weatherData = weatherRecord.Split(';');
        string station = weatherData[0];
        double temperature = double.Parse(weatherData[1]);
        
        if (StationStatistics.TryGetValue(station, out Statistics statistic)) 
        { 
             statistic.UpdateStatistics(temperature);
             return;
        }
        
        Statistics statistics = new Statistics(temperature); 
        StationStatistics.Add(station, statistics);
    }
    
}