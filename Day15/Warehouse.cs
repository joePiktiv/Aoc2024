using System.Runtime.InteropServices;
using System.Text;
using Tools;

namespace Day15;

public class Warehouse
{
    public Map Map { get; set; }
    public string Moves { get; set; }
    public int[] Robot { get; set; }
    public List<int[]> Walls { get; set; }
    public List<int[]> Boxes { get; set; }

    public Warehouse(List<string> lines)
    {
        var split = string.Join(Environment.NewLine, lines).Split(new string[]{ "\r\n\r\n" },
        StringSplitOptions.RemoveEmptyEntries);
        Map = new Map(split[0].Split(Environment.NewLine).ToList());
        Moves = split[1].Replace("\r\n", "");
        Robot = Map.FindAll('@').First();
        Walls = Map.FindAll('#');
        Boxes = Map.FindAll('O');
        do
        {
            Move();
            Robot = Map.FindAll('@').First();
            
        }
        while (Moves.Length > 0);
        var posOfOs = Map.FindAll('O');
        var sum =posOfOs.Select(p => p[0] * 100 + p[1]).Sum();
        Console.WriteLine("Part 1 sum is "+sum);
    }

    public void Move()
    {
        if (Moves.Length == 0) return;
        var next = Moves[0];
        Moves = Moves.Substring(1);
        var line = (next == '^' || next == 'v') ? Map.GetColValues(Robot[1]) : Map.GetRowValues(Robot[0]); 
        var indexRobot = line.IndexOf('@');
        
        List<char> str;
        if (next == '^' || next == '<')
        {
            str = line.Slice(0, indexRobot+1);
            str.Reverse();
        }
        else str = line.Slice(indexRobot, line.Count - indexRobot);

        var firstWall = str.IndexOf('#');
        var beforeFirstWall = str.Slice(0,firstWall);
        var afterFirstWall = str.Slice(firstWall, str.Count - firstWall);
        
        var nextDot = beforeFirstWall.IndexOf('.');
        if (nextDot == -1) return;
        var beforeDot = beforeFirstWall.Slice(0, nextDot);
        var afterDot = beforeFirstWall.Slice(nextDot + 1, beforeFirstWall.Count - nextDot - 1);
        var newLine = (new List<char> { '.' }).Concat(beforeDot).Concat(afterDot).Concat(afterFirstWall).ToList();

        if (next == '^' || next == '<')
        {
            newLine.Reverse();
            newLine = newLine.Concat(line.Slice(indexRobot + 1, line.Count - indexRobot - 1)).ToList();
        }
        else
        {
            var nfl = line.Slice(0, indexRobot);
            newLine = nfl.Concat(newLine).ToList();
        }

        if (next == '^' || next == 'v')
        {
            for (int i = 0; i < newLine.Count; i++)
            {
                var col = Robot[1];
                var sb = new StringBuilder(Map.Lines[i]);
                sb[col] = newLine[i];
                Map.Lines[i] = sb.ToString();
            }
        } 
        else
        {
            string ny = new string(newLine.ToArray());
            Map.Lines[Robot[0]] = ny;
        }
    }
}