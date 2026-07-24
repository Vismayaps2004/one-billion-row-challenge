using OneBillionRowChallenge;

public class WeatherProcessor
{

    Dictionary<string, Statistics> stationStatistics = new();
    public void Process()
    {
        var data = "tokyo;36";
        var weatherData = data.Split(';');
        GenerateStatistics(weatherData);
    }

    private void GenerateStatistics(string[] weatherData)
    {
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