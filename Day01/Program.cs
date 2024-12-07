using System.Xml.Schema;
using Tools;

namespace Day1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var simplePath = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day01\\simple.txt";
        var question1Path = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day01\\question1.txt";
        
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day06\\simple.txt";
        var lines = FileReaderClass.ReadFromFile(simplePath);
        //Console.WriteLine(lines.Count);
        var result = new TwoPaths(lines);
    }
}