using Tools;

namespace Day02;
class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day02\\simple.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day02\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var reports = lines.Select(l => new Report(l, true)).ToList();
        Console.WriteLine("Part 1 "+reports.Count(r => r.IsSafe ) );
        Console.WriteLine("Part 2 "+reports.Count(r => r.BliSafe ) );
    }
}