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
        Statistics statistics = new Statistics ();
        var temp = int.Parse(parsedData[1]);
        Statistics statistics1 = statistics.StoreStatistics(30);
        stationStatistics.Add("tokyo", statistics1);
        if (stationStatistics.ContainsKey(parsedData[0])) 
        {
            statistics.UpdateStatistics(temp);
        }
        else
        {
            Statistics statisticsnew = new Statistics ();
            Statistics statistics2 = statisticsnew.StoreStatistics(temp);
            stationStatistics.Add(parsedData[0], statistics2);    
        }

    }
    
}