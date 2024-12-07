using Tools;

namespace Day07;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day07\\simple.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day07\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        
        var equations1 = lines.Select(l => new Equation(l,1)).ToList();
        var sum1 = equations1.Where(e => e.IsSolved).Sum(e => e.Target);
        Console.WriteLine("sum1 solvable is "+ sum1);
        
        var equations2 = lines.Select(l => new Equation(l,2)).ToList();
        var sum2 = equations2.Where(e => e.IsSolved).Sum(e => e.Target);
        Console.WriteLine("sum2 solvable is "+ sum2);
    }
}