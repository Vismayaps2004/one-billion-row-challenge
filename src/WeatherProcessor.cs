using OneBillionRowChallenge;

public class WeatherProcessor
{

    Dictionary<string, Statistics> stationStatistics = new();
    public void Process()
    {
        var data = "tokyo;36";
        var parsedData = data.Split(';').ToList();
        GenerateStatistics(parsedData);
    }

    private void GenerateStatistics(List<string> parsedData)
    {
        var temp = int.Parse(parsedData[1]);
        if (stationStatistics.ContainsKey(parsedData[0])) 
        {
            stationStatistics[parsedData[0]].StoreStatistics(temp);
        }
        else
        {
            Statistics statistics = new Statistics ();
            statistics.StoreStatistics(temp);
            stationStatistics.Add(parsedData[0], statistics);    
        }

    }
    
}