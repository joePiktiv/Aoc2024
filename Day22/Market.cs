namespace Day22;

public class Market
{
    public List<Secret> Monkeys { get; set; }
    public Dictionary<(int, int, int, int), int> Cache { get; set; }
    public int Highest { get; set; }
    public long Num2000Sum { get; set; }
    public Market(List<string> lines)
    {
        var limit = 2000;
        Monkeys = lines.Select(x => new Secret(long.Parse(x), 2000)).ToList();
        var wholeSeq = Monkeys.SelectMany(m => m.Sequences).ToList();
        Cache = new Dictionary<(int, int, int, int), int>();
        Highest = 0;
        Num2000Sum = Monkeys.Sum(s => s.Num2000);
        // Console.WriteLine($"part 1  {Num2000Sum}");
        // Console.WriteLine($"whole sequence length {wholeSeq.Count}");
        for (int i = 0; i < wholeSeq.Count - 4; i++)
        {
           // if (i % 100000 == 0)
             //   Console.WriteLine($"in progress {i}");
            var seq = wholeSeq.Slice(i, 4);
            if (!Cache.ContainsKey((seq[0], seq[1], seq[2], seq[3])))
            {
                var result = Monkeys.Select(x => x.GetFromCache(seq)).Sum();
                // if (result > Highest)
                //     Console.WriteLine(
                //         $"Sequence {string.Join(',', seq)} and results {string.Join(',', result)} "
                //     );
                Highest = result > Highest ? result : Highest;
                Cache.Add((seq[0], seq[1], seq[2], seq[3]), result);
            }
        }
    }
}
