using Day23;

namespace Day23;

public class Party
{
    public List<Computer> Computers;

    public Party(List<string> lines)
    {
        Computers = new List<Computer>();
        ConnectComputers(lines);

        var list = FindLevelConnection(Computers, 2);
        Console.WriteLine($"Part 1 {list.Count}");

        var largestSet = FindLargestClique(Computers);
        Console.WriteLine($"Part 2 {string.Join(",", largestSet.OrderBy(x => x).ToList())}");
    }

    private List<string> FindLevelConnection(List<Computer> computers, int level)
    {
        var list = Computers.SelectMany(c => c.GetConnections(computers, level)).ToList();
        var distinctList = list
            .GroupBy(arr => string.Join(",", arr.OrderBy(x => x)))
            .Select(group => group.First())
            .Where(com => !IsSelfConnected(com))
            .Where(com => IsInterConnected(com))
            .Where(com => StartWith(com, 't'))
            .ToList();
        var listStr = distinctList
            .Select(x => string.Join(", ", x))
            .ToList();
        return listStr;
    }

    static List<string> FindLargestClique( List<Computer> graph)
    {
        var maxClique = new List<string>();
        var potentialClique = new HashSet<string>();
        var alreadyFound = new HashSet<string>();

        void BronKerbosch(HashSet<string> R, HashSet<string> P, HashSet<string> X)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                if (R.Count > maxClique.Count)
                {
                    maxClique = R.ToList();
                }
                return;
            }

            foreach (var node in P.ToList())
            {
                var neighbors = graph.Find(c => c.Name == node).Connections;
                BronKerbosch(
                    new HashSet<string>(R) { node },
                    new HashSet<string>(P.Intersect(neighbors)),
                    new HashSet<string>(X.Intersect(neighbors))
                );
                P.Remove(node);
                X.Add(node);
            }
        }

        BronKerbosch(new HashSet<string>(), new HashSet<string>(graph.Select(c=> c.Name)), new HashSet<string>());
        return maxClique;
    }

    private bool StartWith(List<string> com, char v)
    {
        return com.Any(c => c.StartsWith(v));
    }

    private bool IsInterConnected (List<string> coms)
        {
            return coms.All(c =>
            {
                var current = Computers.Find(cp => cp.Name == c);
                var rest = coms.Where(cp => cp != c).ToList();
                return rest.All(r => current.Connections.Contains(r));
            });
        }

    private bool IsSelfConnected(List<string> coms)
    {
        return coms.Distinct().Count() < coms.Count;
    }

    private void ConnectComputers(List<string> lines)
    {
        foreach (var line in lines)
        {
            var split = line.Split('-');
            if (Computers.Any(c => c.Name == split[0]))
            {
                Computers.Find(c => c.Name == split[0]).Connect(split[1]);
            }
            else
            {
                var computer = new Computer(split[0]);
                computer.Connect(split[1]);
                Computers.Add(computer);
            }
        }
        Computers.ForEach(c=> c.MutualConnected(Computers));
    }
}