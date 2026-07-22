using OneBillionRowChallenge;

public class WeatherProcessor
{

    Dictionary<string, Statistics> stationStatistics = new();
    public void process()
    {
        var data = "tokyo;36";
        var parsedData = data.Split(';').ToList();
        generateStatistics(parsedData);
    }

    private void generateStatistics(List<string> parsedData)
    {
        var temp = int.Parse(parsedData[1]);
        Statistics statistics1 = Statistics.create(30);
        stationStatistics.Add("tokyo", statistics1);
        if (stationStatistics.ContainsKey(parsedData[0])) 
        {
          Statistics  statistics = stationStatistics[parsedData[0]];
            statistics.updateStatistics(temp);
        }
        else
        {
            Statistics statistics = Statistics.create(temp);
            stationStatistics.Add(parsedData[0], statistics);    
        }

    }
    
}