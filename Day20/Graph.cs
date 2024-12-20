using Tools;
namespace Day19;
public class Graph
{
    private Map Map { get; set; }
    private int[] Start { get; set; }
    private int[] End { get; set; }
    public List<int> SavedP1 { get; set; }
    public List<int> SavedP2 { get; set; }
    public Graph(List<string> lines)
    {
        Map = new Map(lines);
        Start = Map.FindAll('S').First();
        End = Map.FindAll('E').First();
        var innerWalls = GetInnerWalls(Map, '#');
        var (standardSteps, parents) = FindSteps(Map, Start, End, 0);
        var path = parents.Select(p => ((p.Item2, p.Item3), p.Item1 )).ToDictionary();
        var positionCost = path.Select(p => new[] { p.Key.Item1, p.Key.Item2 }).ToList();
        var usefullWalls = RemoveUnrelatedWalls(innerWalls, positionCost, Map);
        SavedP1 = usefullWalls.Select(w => SavesByRemovingTheWall(w, positionCost, parents, Map)-2).ToList();
        SavedP2 = CalculatSaved(Map, path,positionCost, 20, standardSteps);
    }

    private List<int>? CalculatSaved(Map map, Dictionary<(int, int), int> path, List<int[]> positions, int maxCost, int standard)
    {
        var result = new List<int>();
        for (int i = 0; i < path.Count - 1; i++)
        {
            for (int j = i + 1; j < path.Count ; j++)
            {
                var enter = positions[i];
                var exit = positions[j];
                var distance = GetDistance(exit, enter);
                if (distance > maxCost)
                {
                    result.Add(-1);
                    continue;
                }
                var countEnter = path.GetValueOrDefault((enter[0], enter[1]));
                var countExit = path.GetValueOrDefault((exit[0], exit[1]));
                var cost = standard - countExit + countEnter + distance;
                var saved = standard - cost;
                result.Add(saved);
            }
        }
        return result;
    }


    private int GetDistance(int[] pos1, int[] pos2)
    {
        return Math.Abs(pos1[0] - pos2[0]) + Math.Abs(pos1[1] - pos2[1]);
    }

    private int SavesByRemovingTheWall(int[] wall, List<int[]> onPath, HashSet<(int, int, int)> parents, Map map)
    {
        var fourDir = map.GetDirectionsPositions(wall, "4");
        var costs = fourDir.Select(d =>
        {
            var costWithPosition = parents.Where(p => p.Item2 == d[0] && p.Item3 == d[1]).ToList();
            var cost = costWithPosition.Count == 0 ? -1 : costWithPosition[0].Item1;
            return cost;
        }).ToList();
        if (costs[0] >= 0 && costs[1] >= 0) return Math.Abs(costs[0] - costs[1]);
        if (costs[2] >= 0 && costs[3] >= 0) return Math.Abs(costs[2] - costs[3]);
        return 0;
    }

    private List<int[]> RemoveUnrelatedWalls(List<int[]> innerWalls, List<int[]> onPath, Map map)
    {
        return innerWalls.Where(w =>
            map.GetDirectionsPositions(w, "4").Any(p => onPath.Any(pos => map.GetValueOnMap(pos) == map.GetValueOnMap(p) ) ) 
            ||  
            map.GetDirectionsPositions(w, "+l+r").All(p => onPath.Any(pos => map.GetValueOnMap(pos) == map.GetValueOnMap(p) ) ) 
            ).ToList();
    }

    private (int, HashSet<(int, int, int)>) FindSteps(Map map, int[] start, int[] end, int index)
    {
        var queue = new Queue<(int, int, int)>();
        var visited = new HashSet<int[]>();
        var parents = new HashSet<(int, int, int)>();
        var startPos = (0, start[0], start[1]);
        queue.Enqueue(startPos);
        visited.Add(new int[] {start[0], start[1]});
        parents.Add(startPos);
        while (queue.Count > 0)
        {
            var (cost, r,c) = queue.Dequeue();
            var nexts = map.GetDirectionsPositions(new int[] { r, c }, "4");
            var accessable = nexts.Where(n => map.OnBoard(n) && !IsWall(map, n) && !HasVisited(visited, n)).ToList();
            if (HasVisited(accessable.ToHashSet(), end))
            {
                parents.Add((cost + 1, end[0], end[1]));
                return (cost + 1, parents);
            }
            accessable.ForEach(a =>
            {
                queue.Enqueue((cost + 1, a[0], a[1]));
                visited.Add((new[] { a[0], a[1] }));
                parents.Add((cost + 1, a[0], a[1]));
            });
        }

        return (-1, parents);
    }

    private bool HasVisited(HashSet<int[]> visited, int[] pos)
    {
        return visited.Any(p => p[0] == pos[0] && p[1] == pos[1]);
    }

    private bool IsWall(Map m, int[] pos)
    {
        return m.OnBoard(pos) ? m.GetValueOnMap(pos) == '#' : false;
    }

    private List<int[]> GetInnerWalls(Map map, char c)
    {
        return Map.FindAll(c).Where(w => w[0] != 0 && w[1] != 0 && w[0] != (map.Row - 1) && w[1] != (map.Col - 1)).ToList();
    }
}