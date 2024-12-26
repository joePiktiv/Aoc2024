using Tools;

namespace Day16;
class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day16\\sample1.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day16\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var maze = new Maze(lines);
    }
}