using OneBillionRowChallenge;

public class WeatherProcessor
{

    private Dictionary<string, Statistics> stationStatistics = new();
    public Dictionary<string, Statistics> Process(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? weatherRecord = reader.ReadLine();
        while (weatherRecord != null)
        {
            ProcessWeatherStatistics(weatherRecord);
            weatherRecord = reader.ReadLine();
        }
        return stationStatistics; 
        
    }

    private void ProcessWeatherStatistics(string weatherRecord)
    {
        var weatherData = weatherRecord.Split(';');
        string station = weatherData[0];
        double temperature = double.Parse(weatherData[1]);
        
        if (stationStatistics.TryGetValue(station, out Statistics statistic)) 
        { 
             statistic.UpdateStatistics(temperature);
             return;
        }
        
        Statistics statistics = new Statistics(temperature); 
        stationStatistics.Add(station, statistics);
    }
    
}