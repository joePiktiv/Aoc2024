namespace Day22;

public class Secret
{
    public long Init { get; set; }

    public long Num2000 { get; set; }

    public List<int> Prices { get; set; }

    public List<int> Sequences { get; set; }

    public Dictionary<(int, int, int, int), int> Cache { get; set; }

    public Secret(long init, int times)
    {
        Init = init;
        Sequences = new List<int>();
        Prices = new List<int>();
        Cache = new Dictionary<(int, int, int, int), int>();
        var highest = LastDigit(init);
        var prev = init;
        var input = Init;
        GetSequences(input, times, prev);
        GenerateCache();
    }

    void GenerateCache()
    {
        for (int i = 0; i < Sequences.Count - 4; i++)
        {
            var seq = new List<int>
            {
                Sequences[i],
                Sequences[i + 1],
                Sequences[i + 2],
                Sequences[i + 3]
            };
            var seqSe = (Sequences[i], Sequences[i + 1], Sequences[i + 2], Sequences[i + 3]);
            if (!Cache.ContainsKey(seqSe))
            {
                var p = GetPrice(seq);
                Cache.Add(seqSe, p);
            }
        }
    }

    public int GetFromCache(List<int> seq)
    {
        if (Cache.ContainsKey((seq[0], seq[1], seq[2], seq[3])))
            return Cache[(seq[0], seq[1], seq[2], seq[3])];
        return 0;
    }

    public int GetPrice(List<int> seq)
    {
        for (int i = 0; i < Prices.Count - 5; i++)
        {
            var slice = new List<int>
            {
                Prices[i + 1] - Prices[i],
                Prices[i + 2] - Prices[i + 1],
                Prices[i + 3] - Prices[i + 2],
                Prices[i + 4] - Prices[i + 3],
            };
            var isSameSequence = CompareFourSequence(slice, seq);
            if (isSameSequence)
            {
                return Prices[i + 4];
            }
        }
        return 0;
    }

    bool CompareFourSequence(List<int> slice, List<int> seq)
    {
        for (int i = 0; i < seq.Count; i++)
        {
            var s = slice[i];
            var se = seq[i];
            if (se != s)
                return false;
        }
        return true;
    }

    void GetSequences(long input, int times, long prev)
    {
        for (int i = 1; i <= times; i++)
        {
            input = ProcessSecret(input);
            Prices.Add(LastDigit(input));
            Sequences.Add(LastDigit(input) - LastDigit(prev));
            prev = input;
            if (i == 2000)
                Num2000 = input;
        }
    }

    int LastDigit(long input)
    {
        return int.Parse(input.ToString()[input.ToString().Length - 1].ToString());
    }

    public long ProcessSecret(long input)
    {
        var sec = input;
        // step 1
        var step0 = input * 64;
        var step1 = Mix(step0, sec);
        step1 = Prune(step1);
        // step 2
        step0 = step1 / 32;
        var step2 = Mix(step0, step1);
        step2 = Prune(step2);
        // step 3
        step0 = step2 * 2048;
        var step3 = Mix(step0, step2);
        step3 = Prune(step3);
        return step3;
    }

    long Mix(long f1, long f2)
    {
        return f1 ^ f2;
    }

    long Prune(long f1)
    {
        var mod = 16777216;
        while (f1 < mod)
        {
            f1 += mod;
        }

        return f1 % mod;
    }
}
