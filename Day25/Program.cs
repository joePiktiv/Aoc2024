using Tools;

namespace Day24;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day25\\simple.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day25\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var locks = new Office(lines);
        Console.WriteLine($"Part 1 result {locks.Pairs}");
    }
}