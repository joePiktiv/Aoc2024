using Tools;

namespace Day12;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day13\\simple1.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day13\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        string[] gamesStr = string.Join(Environment.NewLine,lines).Split(new string[] { "\r\n\r\n" },
            StringSplitOptions.RemoveEmptyEntries);
        
        var gamesOG = gamesStr.Select(g => new Game(g.Split(Environment.NewLine).ToList(), 0, true)).ToList();
        Console.WriteLine("Part 1 all costs "+gamesOG.Where(g=> g.Cost != -1).Sum(g=> g.Cost));
        
        var gamesNY = gamesStr.Select(g => new Game(g.Split(Environment.NewLine).ToList(), 10000000000000, false)).ToList();
        Console.WriteLine("Part 2 all costs "+gamesNY.Where(g=> g.Cost != -1).Sum(g=> g.Cost));
    }
}