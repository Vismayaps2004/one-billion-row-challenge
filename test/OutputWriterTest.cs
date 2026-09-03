using JetBrains.Annotations;
using OneBillionRowChallenge;

namespace OneBillionRowChallenge.Tests;

[TestSubject(typeof(OutputWriter))]
public class OutputWriterTest
{
    WeatherProcessor weatherProcessor = new WeatherProcessor();
    
    [Fact]
    public void ShouldWriteStatistics()
    {
        string outputPath = "output.txt";
        weatherProcessor.Process("Tokyo;30");
        IEnumerable<KeyValuePair<string, Statistics>> statistics = weatherProcessor.GetStatistics();
        using (OutputWriter outputWriter = new OutputWriter(outputPath, statistics))
        {
            outputWriter.WriteOutput();
        }
        
        string output = File.ReadAllText(outputPath);
        
        Assert.Contains("Station statistics : Tokyo", output);
        Assert.Contains("Min : 30", output);
                Assert.Contains("Mean : 30", output);
                Assert.Contains("Max : 30", output);
        
        File.Delete(outputPath);
    }
    
    [Fact]
    public void ShouldWriteMultipleStatistics()
    {
        string outputPath = "output.txt";
        weatherProcessor.Process("Tokyo;30");
        weatherProcessor.Process("Paris;50");
        
        IEnumerable<KeyValuePair<string, Statistics>> statistics = weatherProcessor.GetStatistics();
        using (OutputWriter outputWriter = new OutputWriter(outputPath, statistics))
        {
            outputWriter.WriteOutput();
        }
        
        string output = File.ReadAllText(outputPath);
        
        Assert.Contains("Station statistics : Tokyo", output);
        Assert.Contains("Min : 30", output);
        Assert.Contains("Mean : 30", output);
        Assert.Contains("Max : 30", output);
        
        Assert.Contains("Station statistics : Paris", output);
        Assert.Contains("Min : 50", output);
        Assert.Contains("Mean : 50", output);
        Assert.Contains("Max : 50", output);
        File.Delete(outputPath);
    }
    
    [Fact]
    public void ShouldWriteMean()
    {
        string outputPath = "output.txt";

        weatherProcessor.Process("Tokyo;30");
        weatherProcessor.Process("Tokyo;40");

        IEnumerable<KeyValuePair<string, Statistics>> statistics =
            weatherProcessor.GetStatistics();

        using (OutputWriter outputWriter = new OutputWriter(outputPath, statistics))
        {
            outputWriter.WriteOutput();
        }

        string output = File.ReadAllText(outputPath);

        Assert.Contains("Min : 30", output);
        Assert.Contains("Mean : 35", output);
        Assert.Contains("Max : 40", output);

        File.Delete(outputPath);
    }
}