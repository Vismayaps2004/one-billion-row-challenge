public class WeatherProcessor
{
    public record Statistics
    (
        double min,
        double max,
        double sum, 
        int count

    );
    Dictionary<string, Statistics> stationStatistics = new();
    public void process()
    {
        var data = "tokyo;36";
        var parsedData = data.Split(';').ToList();
        generateStatistics(parsedData);
    }

    private void generateStatistics(List<string> parsedData)
    {
        if (stationStatistics.ContainsKey(parsedData[0]))
        {
             calculateStatistics(parsedData);
        }

        var temp = int.Parse(parsedData[1]);
        Statistics statistics = new Statistics(temp, temp,temp, 1);
        stationStatistics.Add(parsedData[0], statistics);

    }

    private void calculateStatistics(List<string> stationStatistics)
    {
        Console.WriteLine("inside calculate"); 
    }
}