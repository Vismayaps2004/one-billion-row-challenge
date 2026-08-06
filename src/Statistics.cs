namespace OneBillionRowChallenge;

public class Statistics
{
    
    public double Min { get; private set; }
    public double Max { get; private set; }
    public double Sum { get; private set; }
    public int Count { get; private set; }
    
    public Statistics(double temperature)
    {
        Min = temperature;
        Max = temperature;
        Sum = temperature;
        Count = 1;
    }

    public void Update(double temperature)
    {
        if (Min > temperature)
        {
            Min = temperature;
        }
        if (Max < temperature)
        {
            Max = temperature;
        }
        
        Sum += temperature;
        Count++;
    }
}