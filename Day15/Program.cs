using Tools;

namespace Day15;

class Program
{
    static void Main(string[] args)
    {
        var simple = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day15\\large.txt";
        var test = "C:\\Users\\Joe\\Documents\\Aoc2024\\Aoc2024\\Day15\\test.txt";
        var lines = FileReaderClass.ReadFromFile(test);
        var warehouse = new Warehouse(lines);
        var warehouseV2 = new WarehouseV2(lines);
    }
}