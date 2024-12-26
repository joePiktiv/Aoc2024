using Tools;

namespace Day24;

public class Las
{
    public string Type { get; set; }
    public int[] Code { get; set; }

    public Las(List<string> lines)
    {
        Type = lines[0].All(c => c == '#') ? "lock" : "key";
        var map = new Map(lines);
        Code = Enumerable.Range(0, 5).Select(x => map.GetColValues(x).Count(a => a == '#') - 1).ToArray();
    }

    public bool IsFit(Las las)
    {
        if (Type == las.Type) return false;
        return Enumerable.Range(0, 5).All(x => Code[x] + las.Code[x] <= 5);
    }
}