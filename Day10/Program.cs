using Tools;

namespace Day10;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day10\\simple.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day10\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var graph = new Grid(new Map(lines));
        Console.WriteLine("total Scores "+graph.Scores.Sum());
        Console.WriteLine("total ratings "+graph.Paths.Count);
    }
}