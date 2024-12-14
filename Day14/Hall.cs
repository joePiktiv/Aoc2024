namespace Day14;

public class Hall
{
    public int Vertical { get; set; }
    public int Horizontal { get; set; }
    public List<Robot> Robots { get; set; }
    public int[] Quarters { get; set; }
    public int[] Min { get; set; }
    public int[] Hundred { get; set; }

    public Hall(List<string> lines, int v, int h, int moves)
    {
        Vertical = v;
        Horizontal = h;
        Robots = lines.Select(l => new Robot(l)).ToList();
        Min = new[] { 231221760, 0 };
        
        for (int i = 1; i <= moves; i++)
        {
            Robots.ForEach(r => r.Move(this));
            
            var quarsEach = Robots.Select(r => r.GetQuarter(this)).ToList();
            var quarters = Enumerable.Range(1, 4)
                .Select(n => quarsEach.Count(x => x == n) ).ToArray();
            var agg = quarters.Aggregate(1, (acc, x) => acc * x);
            if (agg > 0 && agg < Min[0]) Min = new[] { agg, i };
            if (i == 100) Hundred = new[] { agg, i };
            if (i == 6771) PrintGraph( i );
        }
    }

    private int CountRobotOnPosition(List<Robot> robots, int i, int j)
    {
        return robots.Count(r => r.Position[0] == i && r.Position[1] == j);
    }

    public void PrintGraph(int indicator)
    {
        Console.WriteLine(indicator);
        for (int i = 0; i < Horizontal; i++)
        {
            for (int j = 0; j < Vertical; j++)
            {
                var hasRobot = isPositionHasRobot(Robots, i, j);
                Console.Write(hasRobot ? "*" : '.');
            }
            Console.WriteLine();
        }
    }

    private bool isPositionHasRobot(List<Robot> robots, int i, int j)
    {
        foreach (var robot in robots)
        {
            if (robot.Position[0] == i && robot.Position[1] == j) return true;
        }
        return false;
    }
}