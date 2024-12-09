using Tools;

namespace Day09;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day09\\simple.txt";
        var superSimple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day09\\superSimple.txt";
        var test =        "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day09\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var disk1 = new Disk(lines[0], 1);
        long checkSum1 = disk1.List.Sum(f => f.GetCheckSum());
        Console.WriteLine("check sum 1 is "+checkSum1);
        var disk2 = new Disk(lines[0], 2);
        long checkSum2 = disk2.List.Sum(f => f.GetCheckSum());
        Console.WriteLine("check sum 2 is "+checkSum2);
    }
}