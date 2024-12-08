using Tools;

namespace Day08;

class Program
{
    static void Main(string[] args)
    {
        var simple = "simple.txt";
        var superSimple = "superSimple.txt";
        var test = "test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        Console.WriteLine(lines.Count);
        var map = new Map(lines);
        var antenna = new Antenna(map);
        var antDos = Map.RemoveDuplicates(antenna.FrequenciesList.SelectMany(f => f.Antinodes).ToList());
        var antDosAdv = Map.RemoveDuplicates(antenna.FrequenciesList.SelectMany(f => f.UpdatedAntinodes).ToList());
        Console.WriteLine("count of anti dos "+antDos.Count);
        Console.WriteLine("count of advanced anti dos "+antDosAdv.Count);
    }
}