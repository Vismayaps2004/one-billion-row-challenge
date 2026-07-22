namespace OneBillionRowChallenge;

public class Statistics
{
    private double min;
    private double max;
    private double sum;
    private double count;

    private Statistics(int temp)
    {
        min = temp;
        max = temp;
        sum = temp;
        count = 1;
    }

    public static Statistics create(int temp)
    {
        return new Statistics(temp);
    }

    public void updateStatistics(int temp)
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