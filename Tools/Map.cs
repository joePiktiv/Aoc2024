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

    public List<char> GetDirectionsValues(int[] pos, string? indicators)
    {
        var positions = GetDirectionsPositions(pos, indicators);
        return positions.Select(p => GetValueOnMap(p)).ToList();
    }

    public List<int[]> GetDirectionsPositions(int[] pos, string? indicators)
    {
        var up = new int[] {pos[0] - 1, pos[1]};
        var down = new int[] {pos[0] + 1, pos[1]};
        var left = new int[] {pos[0] , pos[1] - 1};
        var right = new int[] {pos[0] , pos[1] + 1};
        var leftup = new int[] {pos[0] - 1, pos[1] - 1};
        var rightup = new int[] {pos[0] - 1, pos[1] + 1};
        var leftdown = new int[] {pos[0] + 1, pos[1] - 1};
        var rightdown = new int[] {pos[0] + 1, pos[1] + 1};
        var result = new List<int[]>();
        indicators = indicators == null || indicators == "8" ? "+u+d+l+r-ul-ur-dl-dr" : indicators == "4" ? "+u+d+l+r" : indicators == "diagonals" ? "-ul-ur-dl-dr" : indicators;
        if (indicators.ToLower().Contains("+u")) result.Add(up); 
        if (indicators.ToLower().Contains("+d")) result.Add(down); 
        if (indicators.ToLower().Contains("+l")) result.Add(left); 
        if (indicators.ToLower().Contains("+r")) result.Add(right);
        if (indicators.ToLower().Contains("-ul")) result.Add(leftup); 
        if (indicators.ToLower().Contains("-ur")) result.Add(rightup); 
        if (indicators.ToLower().Contains("-dl")) result.Add(leftdown); 
        if (indicators.ToLower().Contains("-dr")) result.Add(rightdown);
        return result.Where(r => OnBoard(r)).ToList();
    }
    

    public List<char> GetColValues(int col)
    {
        return Lines.Select(l => l[col]).ToList();
    }

    public List<char> GetRowValues(int row)
    {
        return Lines[row].ToCharArray().ToList();
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
    public void PrintPaths(List<List<int[]>> paths)
    {
        paths.ForEach(path => PrintPath(path));
    }

    public void PrintPath(List<int[]> path)
    {
        Console.WriteLine();
        path.ForEach(pos => Console.Write($"({pos[0]},{pos[1]})")); 
    }

    public bool SamePosition(int[]? first, int[] second)
    {
        return (first[0] == second[0] && first[1] == second[1]);
    }
}