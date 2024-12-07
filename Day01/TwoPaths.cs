namespace Day1;

public class TwoPaths
{
    public List<int> Path1 { get; set; }
    public List<int> Path2 { get; set; }
    
    public TwoPaths(List<string> lines)
    {
        Path1 = new List<int>();
        Path2 = new List<int>();
        var distances = 0;
        var similarity = 0;
        foreach (var line in lines)
        {
            var split = line.Split("   ");
            var first = int.Parse(split[0].Trim(' '));
            var second = int.Parse(split[1].Trim(' '));
            Path1.Add(first);
            Path2.Add(second);
        }
        Path1.Sort();
        Path2.Sort();
        for (int i = 0; i < Path1.Count; i++)
        {
            var first = Path1[i];
            var secdond = Path2[i];
            var distance = Math.Abs(first - secdond);
            var timesInPath2 = Path2.Count(value => first == value);
            similarity += first * timesInPath2;
            distances += distance;
        }
        Console.WriteLine("Part one result : " + distances);
        Console.WriteLine("Part two result : " + similarity);
    }
}