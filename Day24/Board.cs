namespace Day24;

public class Board
{
    private Dictionary<string, int> Dic { get; set; }
    public long Part1Result { get; set; }
    public string Part2Result { get; set; }
    public Board(List<string> lines)
    {
        var keys = new List<string>();
        var breaker = lines.IndexOf("");
        Dic = Translate(new List<string>(lines));
        var (xl, yl, zl, x, y, z) = CalculateXYZ(Dic);
        Part1Result = Convert.ToInt64(z,2);
        Part2Result = Operator.FindGates(lines);
    }

  
    private (List<string>, List<string>, List<string>, string, string, string) CalculateXYZ(Dictionary<string, int> dic)
    {
        var xs = GetBinaryString('x', dic);
        var ys = GetBinaryString('y', dic);
        var zs = GetBinaryString('z', dic);
        var xStr = string.Join("", xs);
        var yStr = string.Join("", ys);
        var zStr = string.Join("", zs);
        return (xs, ys, zs, xStr, yStr, zStr);
    }
    
    private Dictionary<string, int>? Translate(List<string> lines)
    {
        var breaker = 200;
        var counter = 0;
        var isBase = true;
        var dic = new Dictionary<string, int>();
        while (lines.Count > 0)
        {
            var line = lines[0];
            if (counter > breaker) return new Dictionary<string, int>();
            lines = lines.Slice(1, lines.Count - 1);
            if (line.Length == 0)
            {
                isBase = false;
                continue;
            }
            if (isBase)
            {
                var split = line.Split(": ");
                dic.Add(split[0], int.Parse(split[1]));
            }
            else
            {
                var tryWire = Wire.TryGetWire(line, dic);
                if (tryWire)
                {
                    var (k, v) = Wire.GetWire(line, dic);
                    dic.Add(k,v);
                    counter = 0;
                }
                else
                {
                    lines.Add(line);
                    counter++;
                }
            }
        }
        return dic;
    }
    
    List<string> GetBinaryString (char c, Dictionary<string, int> dic) => dic.Where((k) => k.Key.StartsWith(c)).OrderByDescending(k => k.Key).Select(k => k.Value.ToString()).ToList();
}