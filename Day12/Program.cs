using Tools;

namespace Day12;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day12\\simple3.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day12\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var garden = new Garden(lines);
        Console.WriteLine(garden.Groups + " cost by length " + garden.CostsByLengths + " by sides " + garden.Sizes.Sum() );
    }
}