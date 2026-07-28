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
        var statisticsMap = weatherProcessor.Process(args[0]);
        Console.WriteLine("station statistics : "+ statisticsMap);
    }
}