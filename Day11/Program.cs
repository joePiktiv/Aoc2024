
using Tools;

namespace Day11;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\solfa\\Documents\\GitHub\\Aoc2024\\Day11\\simple.txt";
        var test = "C:\\Users\\solfa\\Documents\\GitHub\\Aoc2024\\Day11\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var part1 = new Seed(lines[0], 25);
        var part2 = new Seed(lines[0], 75);
        Console.WriteLine("Part 1 result " + part1.Count);
        Console.WriteLine("Part 2 result " + part2.Count);
    }
}