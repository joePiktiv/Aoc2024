using Tools;

namespace Day22;

public class Program
{
    static void Main(string[] args)
    {
        var simple = "simple.txt";
        var test = "test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var market = new Market(lines);
        Console.WriteLine($"Part 1 {market.Num2000Sum}");
        Console.WriteLine($"Part 2 {market.Highest}");
    }
}