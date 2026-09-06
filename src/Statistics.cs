namespace OneBillionRowChallenge;

public class Statistics(int temperature)
{
    
    public int Min { get; private set; } = temperature;
    public int Max { get; private set; } = temperature;
    public int Sum { get; private set; } = temperature;
    public int Count { get; private set; } = 1;

    public void Update(int temperature)
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

    public int Mean()
    {
        return Sum / Count;
    }
    
}