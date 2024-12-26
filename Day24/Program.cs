using Tools;

namespace Day24;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day24\\p2s.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day24\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var board = new Board(lines);
        Console.WriteLine($"Part 1 {board.Part1Result}");
        Console.WriteLine($"Part 2 {board.Part2Result}");
    }
}