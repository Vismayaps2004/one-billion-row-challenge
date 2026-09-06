namespace OneBillionRowChallenge;

public class WeatherProcessor
{

    private readonly Dictionary<string, Statistics> stationStatistics = new();
    public void Process(string weatherRecord)
    {
        var separatorIndex = weatherRecord.IndexOf(';');
        var station = weatherRecord.AsSpan().Slice(0,separatorIndex);
         int temperatureSpan = ParseTemperature(weatherRecord.AsSpan(separatorIndex + 1));

        Dictionary<string, Statistics>.AlternateLookup<ReadOnlySpan<char>> lookup = stationStatistics.GetAlternateLookup<ReadOnlySpan<char>>();
        if (lookup.TryGetValue(station, out Statistics statistic)) 
        { 
             statistic.Update(temperatureSpan);
             return;
        }
        
        Statistics statistics = new Statistics(temperatureSpan); 
        stationStatistics.Add(station.ToString(), statistics);
    }

    private static int ParseTemperature(ReadOnlySpan<char> temperature)
    {
        var value = 0;
        bool isNegative = false;
        foreach (char ch in temperature)
        {
            switch (ch)
            {
                case '-':
                    isNegative = true;
                    break;
                case '.':
                    break;
                default:
                    int digit = ch - '0';
                    value =  value * 10 + digit;
                    break;
            }
            
        }
        
        return isNegative ? -value :value;
    }

    public IEnumerable<KeyValuePair<string, Statistics>> GetStatistics()
    {
        Console.WriteLine(stationStatistics.Count);
        return stationStatistics;
    }
}