
using Tools;

namespace Day19;

class Program
{
    static void Main(string[] args)
    {
        const string simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day20\\simple.txt";
        const string test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day20\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var maze = new Graph(lines);
        Console.WriteLine($"Part 1 result {maze.SavedP1.Where(s => s >=  100).Count()}");
        Console.WriteLine($"Part 2 result {maze.SavedP2.Where(s => s >=  100).Count()}");
    }
}