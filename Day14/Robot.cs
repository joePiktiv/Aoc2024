using System.Runtime.CompilerServices;

namespace Day14;

public class Robot
{
    public int[] Position { get; set; }
    public int[] Direction { get; set; }

    public Robot(string line)
    {
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var pos = split[0].Split('=')[1].Split(',');
        var dir =split[1].Split('=')[1].Split(',');
        Position = new int[] { int.Parse(pos[0]), int.Parse(pos[1]) };
        Direction = new int[] { int.Parse(dir[0]), int.Parse(dir[1]) };
    }

    public void Move(Hall hall)
    {
        Position = new int[]
        {
            (Position[0] + Direction[0] + hall.Horizontal ) % hall.Horizontal, (Position[1] + Direction[1] + hall.Vertical) % hall.Vertical
        };
    }

    public int GetQuarter(Hall hall)
    {
        var midV = hall.Vertical / 2;
        var midH = hall.Horizontal / 2;
        var v = Position[0];
        var h = Position[1];
        if (h < midV && v < midH) return 1;
        if (h > midV && v < midH) return 3;
        if (h < midV && v > midH) return 2;
        if (h > midV && v > midH) return 4;
        return 0;
    }
}