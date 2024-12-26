
using Tools;

namespace Day21; 

public class Program
{

    static async Task Main(string[] args)
    {
        var test = "test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var part1 = new Part1(lines);
        var part2 = new Part2(lines);
    }
}