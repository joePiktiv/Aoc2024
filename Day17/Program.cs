using Tools;

namespace Day17;

class Program
{
    static void Main(string[] args)
    {
        var simple = "simple.txt";
        var test = "test.txt";
        var t2 = "t2.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var com = new Computer(lines);
        var tp2 = new Part2();
        Console.WriteLine($"Part 1 result {string.Join(',', com.Output)}");
        Console.WriteLine($"Part 2 result {tp2.Revers()}");
    }
}