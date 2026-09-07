namespace OneBillionRowChallenge;

public class WeatherProcessor
{

    private readonly Dictionary<int, Statistics> stationStatistics = new();
    public void Process(ReadOnlySpan<byte> weatherRecord)
    {
        var separatorIndex = weatherRecord.IndexOf((byte)';');
        var station = weatherRecord[..separatorIndex];
        ReadOnlySpan<byte> temperature = weatherRecord[(separatorIndex + 1)..];
        int temperatureValue = ParseTemperature(temperature);
        int stationHashCode = ComputeHash(station);

        if (stationStatistics.TryGetValue(stationHashCode, out Statistics statistic)) 
        { 
             statistic.Update(temperatureValue);
             return;
        }
        
        Statistics statistics = new Statistics(temperatureValue, station.ToString()); 
        stationStatistics.Add(stationHashCode, statistics);
    }

    private static int ComputeHash(ReadOnlySpan<byte> station)
    {
        int hash = 17;
        foreach (byte b in station)
        {
            hash = hash * 31 + b;
        }
        return hash;
    }

    private static int ParseTemperature(ReadOnlySpan<byte> temperature)
    {
        var value = 0;
        bool isNegative = false;
        foreach (byte ch in temperature)
        {
            switch (ch)
            {
                case (byte)'-':
                    isNegative = true;
                    break;
                case (byte)'.':
                    break;
                default:
                    int digit = ch - (byte)'0';
                    value =  value * 10 + digit;
                    break;
            }
        }
        return isNegative ? -value :value;
    }

    public IEnumerable<KeyValuePair<int, Statistics>> GetStatistics()
    {
        Console.WriteLine(stationStatistics.Count);
        return stationStatistics;
    }
}