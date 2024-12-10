using System.Linq;
using Tools;

namespace Day10;

public class Graph
{
    public Map Map { get; set; }
    public List<List<int[]>> Paths { get; set; }
    public List<int[]> Starts { get; set; }
    public List<int> Scores { get; set; }

    public Graph(Map map)
    {
        Map = map;
        Starts = FindStarts(map);
        Paths = Starts.Select(s => new List<int[]> { s }).ToList();
        do Move();
        while (Paths.Any(p => Map.GetValueOnMap(p.Last()) != '9'));
        Scores = CountTrails(Starts, Paths);
    }

    private List<int> CountTrails(List<int[]> starts, List<List<int[]>> paths)
    {
        return starts.Select(start =>
                Map.RemoveDuplicates(Paths
                        .Where(p => Map.SamePosition(p.First(), start))
                        .Select(p => p.Last()))!
                    .Count)
            .ToList();

    }

    public void PrintPaths(List<List<int[]>> paths)
    {
        paths.ForEach(path => { path.ForEach(pos => Console.Write($"({pos[0]},{pos[1]})")); Console.WriteLine(); });

    }

    public void Move()
    {
        Paths = Paths.SelectMany(path =>
        {
            var current = path.Last();
            return Map.GetValueOnMap(current) == '9'
                ? new[] { path }
                : Moveable(Map, current)
                    .Select(p => path.Concat(new List<int[]> { p }).ToList());
        }).ToList();
    }

    public List<int[]> Moveable(Map map, int[] pos)
    {
        var target = int.Parse(map.GetValueOnMap(pos).ToString()) + 1;
        var directions = FourDirections(pos, map);
        return directions.Where(x => int.Parse(map.GetValueOnMap(x).ToString()) == target).ToList();
    }

    public List<int[]> FourDirections(int[] pos, Map map)
    {
        var up = new int[] { pos[0] - 1, pos[1] };
        var right = new int[] { pos[0], pos[1] + 1 };
        var down = new int[] { pos[0] + 1, pos[1] };
        var left = new int[] { pos[0], pos[1] - 1 };
        return (new List<int[]> { up, right, down, left }).Where(pos => map.OnBoard(pos)).ToList();
    }

    public List<int[]> FindStarts(Map map)
    {
        return Enumerable.Range(0, map.Row)
            .SelectMany(r => Enumerable.Range(0, map.Col)
                .Select(c => new int[] { r, c }))
                .Where(p => map.GetValueOnMap(p) == '0')
                .ToList(); 
    }
}
