using Tools;

namespace Day14;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day14\\simple.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day14\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var hall = new Hall(lines, 103, 101, 10000);
        Console.WriteLine("Part 1 result " + hall.Hundred[0]);
        Console.WriteLine("Part 2 result " + hall.Min[1]);
    }
}