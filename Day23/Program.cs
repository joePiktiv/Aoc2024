using Tools;

namespace Day23;

class Program
{
    static void Main(string[] args)
    {
        var simple = "simple.txt";
        var test = "pk.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        Console.WriteLine(lines.Count);
        var party = new Party(lines);
    }
}