namespace Day24;

public class Operator
{
    private record Triple
    {
        public string Original { get; set; }
        public string Lhs { get; set; }
        public string Rhs { get; set; }
        public string Op { get; set; }
        public string Target { get; set; }
    }

    public static string FindGates(List<string> lines)
    {
        var triples = new List<Triple>();
        foreach (var line in lines)
        {
            if (line.Contains("->"))
            {
                var parts = line.Split(" -> ");
                var expression = parts[0].Split(' ');

                triples.Add(
                    new Triple
                    {
                        Original = line,
                        Lhs =
                            String.Compare(expression[0], expression[2], StringComparison.Ordinal)
                            < 0
                                ? expression[0]
                                : expression[2],
                        Rhs =
                            String.Compare(expression[0], expression[2], StringComparison.Ordinal)
                            > 0
                                ? expression[0]
                                : expression[2],
                        Target = parts[1],
                        Op = expression[1]
                    }
                );
            }
        }

        var answer = new HashSet<string>();
        foreach (var triple in triples)
        {
            if (triple.Target.StartsWith('z') && triple.Op != "XOR" && triple.Target != "z45")
            {
                answer.Add(triple.Target);
            }

            if (!triple.Target.StartsWith('z') && triple.Op == "XOR" && !triple.Lhs.StartsWith("x"))
            {
                answer.Add(triple.Target);
            }
            if (triple.Op == "AND" && triple.Lhs != "x00")
            {
                var feeds = triples
                    .Where(t => t.Lhs == triple.Target || t.Rhs == triple.Target)
                    .ToList();
                foreach (var fed in feeds)
                {
                    if (fed.Op != "OR")
                    {
                        answer.Add(triple.Target);
                        break;
                    }
                }
            }
            if (triple.Op == "OR")
            {
                var LHSFeeds = triples.Single(t => t.Target == triple.Lhs);
                if (LHSFeeds.Op != "AND")
                {
                    answer.Add(LHSFeeds.Target);
                }

                var RHSFeeds = triples.Single(t => t.Target == triple.Rhs);
                if (RHSFeeds.Op != "AND")
                {
                    answer.Add(RHSFeeds.Target);
                }
            }
        }
        var a = answer.ToList();
        a.Sort();
        return string.Join(',', a);
    }
}