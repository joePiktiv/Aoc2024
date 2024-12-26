using Day18;
using Tools;

class Program
{
    static void Main(string[] args)
    {
        var simple = "simple.txt";
        var test = "test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var part1 = new Run(lines, 71, 71, 1024);
        Console.WriteLine($"Part 1 {part1.Step}");
        for (int i = 3033; i < lines.Count; i++)
        {
            var run = new Run(lines, 71, 71, i);
            if (run.Step == -1)
            {
                Console.WriteLine("Part 2 " + lines[i - 1]);
                break;
            }
        }
    }
}