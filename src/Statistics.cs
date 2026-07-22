namespace OneBillionRowChallenge;

public class Statistics
{
    private double min;
    private double max;
    private double sum;
    private double count;
    
    

    public Statistics StoreStatistics(double temp)
    {
        min = temp;
        max = temp;
        sum = temp;
        count = 1;
    }

    public void UpdateStatistics(int temp)
    {
        Console.WriteLine($"{count}: {min} - {max} = {sum} - {count}");
        if (min > temp)
        {
            min = temp;
        }
        if (max < temp)
        {
            max = temp;
        }
        
        sum += temp;
        count++;
    }
}