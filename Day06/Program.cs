using Tools;

namespace Day06;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day06\\simple.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day06\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        Console.WriteLine(lines.Count);
        var map = new Map(lines);
        var guard = new Guard(map, true);
        var current = guard.Position;
        Console.WriteLine("part 1 "+"distinct positions on path "+guard.Distincts.Count );
        Console.WriteLine("part 2 "+"no. of obs to create loop path "+guard.Obses.Count);
    }
}