namespace Tools;

public class Map
{
    public List<string> Lines { get; set; }

    public int Row {get; set;}
    public int Col {get; set;}

    public Map (List<string> lines)
    {
        Lines = lines;
        Row = Lines.Count;
        Col = Lines[0].Length;
    } 

    public char GetValueOnMap (int r, int c)
    {
        return Lines[r][c];
    }    
    public char GetValueOnMap (int[] position)
    {
        return Lines[position[0]][position[1]];
    }

    public bool OnBoard (int[] p)
    {
        var r = p[0];
        var c = p[1];
        return ((r>=0 && r<Row) && (c<Col && c >= 0));
    }
    public static List<int[]>? RemoveDuplicates(IEnumerable<int[]> input)
    {
        var result = new List<int[]>();
        foreach (var point in input)
        {
            if (ExistPointOnPath(result, point)) continue;
            result.Add(point);
        }
        return result;
    }

    public static bool ExistPointOnPath(List<int[]> result, int[] point)
    {
        foreach (var p in result)
        {
            if (p[0] == point[0] && p[1] == point[1]) return true;
        }

        return false;
    }

    public bool SamePosition(int[]? first, int[] second)
    {
        return (first[0] == second[0] && first[1] == second[1]);
    }
}