public class WeatherProcessor
{
    public void process()
    {
        var data = "tokyo;36";
        var parsedData = data.Split(';').ToList();
        Console.WriteLine(parsedData[0]);
    }
}