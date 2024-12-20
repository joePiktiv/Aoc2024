namespace Day19;

public class Design
{
    public List<long> Solvables { get; set; }
    private Dictionary<string, long> Cache { get; set; }
    public Design (List<string> lines)
    {
        Cache = new Dictionary<string, long>();
        var bases = lines[0].Split(", ").ToList();
        var patterns = lines.Slice(2, lines.Count - 2).ToList();
        Solvables = patterns.Select(p => SolvePattern(bases, p)).ToList();
    }
    private long SolvePattern(List<string> bases, string input)
    {
        if (Cache.TryGetValue(input, out long value)) return value;
        if (input.Length == 0) return 1;
        var firstClaster = bases.Where(b => input.StartsWith(b)).ToList();
        var restClaster = firstClaster.Select(f => f.Length >= input.Length ? "" : input.Substring(f.Length)).ToList();
        var num = restClaster.Sum(r => SolvePattern(bases, r));
        Cache.Add(input, num);
        return num;
    }
}