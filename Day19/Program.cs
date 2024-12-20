using Tools;

namespace Day19;
class Program
{
    static void Main(string[] args)
    {                
        const string simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day19\\simple.txt";
        const string test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day19\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var pattern = new Design(lines);
        Console.WriteLine($"Part 1 Num of solvable {pattern.Solvables.Count(s => s != 0)}");
        Console.WriteLine($"Part 2 Count of solvable {pattern.Solvables.Sum()}");
    }
}