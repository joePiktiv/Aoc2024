namespace Day11;

public class Seed
{
    public long Count { get; set; }
    public Dictionary<(string, int), long> Cache { get; set; }

    public Seed(string line, int times)
    {
        var stones = line.Split(' ').Select(s => s.Trim()).ToList();
        Cache = new Dictionary<(string, int), long>();
        Count = stones.Sum(s => CountStones(s, times));
    }

    public long CountStones(string input, int times)
    {
        var keys = (input, times);
        if (times == 0) return 1;
        if (Cache.ContainsKey(keys)) return Cache[keys];
        var count = ApplyRules(input).Sum(i => CountStones(i, times - 1));
        Cache.Add(keys, count);
        return count;
    }

    public string RemoveLeadingZeros(string input) => long.Parse(input).ToString();

    public List<string> ApplyRules(string input)
    {
        if (long.Parse(input) == 0) return new List<string> { "1" };
        if (input.Length % 2 == 0)
            return new List<string>
            {
                RemoveLeadingZeros(input.Substring(0, input.Length / 2)),
                RemoveLeadingZeros(input.Substring(input.Length / 2))
            };
        return new List<string> { (long.Parse(input) * 2024).ToString() };
    }
}
