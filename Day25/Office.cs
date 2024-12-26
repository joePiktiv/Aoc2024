namespace Day24;

public class Office
{
    public List<Las> laser { get; set; }
    public int Pairs { get; set; }
    public Office(List<string> lines)
    {
        laser = GetLaser(lines);
        var locks = laser.Where(l => l.Type == "lock").ToList();
        var keys = laser.Where(l => l.Type == "key").ToList();
        Pairs = Enumerable.Range(0, locks.Count).Select(l => keys.Count(key => key.IsFit(locks[l]) )).Sum();
    }

    List<Las> GetLaser(List<string> lines)
    {
        var lasx = new List<Las>();
        var las = new List<string>();
        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                lasx.Add(new Las(las));
                las = new List<string>();
            } else las.Add(line);
        }
        lasx.Add(new Las(las));

        return lasx;
    }
}