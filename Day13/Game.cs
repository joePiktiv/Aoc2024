using Day12;
using Microsoft.VisualBasic;

public class Game
{
    public long Cost { get; set; }
    public List<Rule> Rules { get; set; }
    public long[] Target { get; set; }
    public bool IsLowerThan100 { get; set; }

    public Game(List<string> lines, long correction, bool isLowerThan100)
    {
        IsLowerThan100 = isLowerThan100;
        // Correction = correction;
        Rules = lines.Where(l => l.StartsWith("Button")).Select(l => new Rule(l, "Button")).ToList();
        Target = GetTarget(lines, "Prize", correction);
        Rules.ForEach(r => r.GetMaxPush(this));
        Cost = CalculatePresses();
    }

    private long CalculatePresses()
    {
        var (p1lead, p1follow, found1) = CalculateX(Rules[0], Rules[1]);
        var (p2lead, p2follow, found2) = CalculateX(Rules[1], Rules[0]);
        var tokensP1 = found1 ? p1lead * 3 + p1follow  : -1;
        var tokensP2 =  found2 ? p2lead + p2follow * 3  : -1;
        //if (p1lead > 100 || p2lead > 100) return -1;
        var allCosts = new List<long> { tokensP1, tokensP2 };
        if (allCosts.All(c => c == -1)) return -1;
        var cost =  allCosts.Where(t => t != -1).Min() ;
        return cost;
    }

    private (long, long,  bool) CalculateX(Rule lead, Rule follow)
    {
        var ax = lead.X;
        var ay = lead.Y;
        var bx = follow.X;
        var by = follow.Y;
        var px = Target[0];
        var py = Target[1];
        
        var leadPress = (px * by - py * bx) / (ax * by - ay * bx);
        var followPress = (px - ax * leadPress ) / bx;
        if (leadPress % 1 == 0 && followPress % 1 == 0 && leadPress * ax + followPress * bx == px && leadPress * ay + followPress * by == py )
        {
            if (IsLowerThan100 && (leadPress >= 100 || followPress >= 100)) return (-1, -1, false);
            if (leadPress < 0 || followPress < 0) return (-1, -1, false);
            return (leadPress, followPress, true);
        }
        return (-1, -1, false);
    }
    private (long, long,  bool) Calculate(Rule lead, Rule follow)
    {
        for (int i = lead.MaxPress; i >= 0; i--)
        {
            var leadPress = i;
            var xDis = lead.X * i;
            var yDis = lead.Y * i;
            var remainX = Target[0] - xDis;
            var remainY = Target[1] - yDis;
            int followPress = (int)remainX / follow.X;
            if (remainX == follow.X * followPress && remainY == follow.Y * followPress)
                if (leadPress % 1 == 0 && followPress % 1 ==0)
                {
                    if (IsLowerThan100 && (leadPress > 100 || followPress > 100)) return (-1, -1, false);
                    if (leadPress < 0 || followPress < 0) return (-1, -1, false);
                    Console.WriteLine("found solution lead " + leadPress + " follow " + followPress);
                    return (leadPress, followPress, true);
                }
        }
        return (-1, -1, false);
    }

    private long[]? GetTarget(List<string> lines, string key, long correction)
    {
        var line = lines.Where(l => l.StartsWith(key)).First();
        var splitsPrize = line.Split(": ")[1].Split(", ");
        var x = GetValueStartWith(splitsPrize, "X=");
        var y = GetValueStartWith(splitsPrize, "Y=");
        return new long[] { x + correction, y + correction};
    }

    public static int GetValueStartWith(string[] strs, string keyword)
    {
        var line = strs.Where(s => s.StartsWith(keyword)).First();
        var value = line.Substring(keyword.Length);
        return int.Parse(value);
    }
}