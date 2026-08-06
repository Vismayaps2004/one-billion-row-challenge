namespace  OneBillionRowChallenge;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 1BRC === ");
        if (args.Length == 0)
        {
            Console.WriteLine("provide file path");
            return ;
        }

        WeatherProcessor weatherProcessor = new WeatherProcessor();
        WeatherRecordReader weatherRecordReader = new WeatherRecordReader(args[0]);
        var weatherRecord = weatherRecordReader.ReadLine();
        while (weatherRecord != null)
        {
            weatherProcessor.Process(weatherRecord);
            weatherRecord = weatherRecordReader.ReadLine();
        }
        weatherRecordReader.Dispose();
    }
}