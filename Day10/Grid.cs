using Tools;

namespace Day10;
public class Grid
{
    public List<int> Scores { get; }
    public List<List<int[]>> Paths { get; }
    public Grid(Map map)
    {
        var starts = map.FindAll('0');
        var paths = starts.Select(s => new List<int[]> { s }).ToList();

        while (paths.Any(p => map.GetValueOnMap(p.Last()) != '9'))
            paths = ExpandPaths(paths, map);

        Paths = paths;

        Scores = starts.Select(start => paths
                .Where(p => p.First() == start)
                .Select(p => string.Join(",", p.Last()))
                .Distinct()
                .Count())
            .ToList();
    }

    private List<List<int[]>> ExpandPaths(List<List<int[]>> paths, Map map) =>
        paths.SelectMany(path =>
        {
            var current = path.Last();
            return map.GetValueOnMap(current) == '9'
                ? new[] { path }
                : GetMovablePositions(map, current)
                    .Select(next => path.Append(next).ToList());
        }).ToList();

    private List<int[]> GetMovablePositions(Map map, int[] pos) =>
        map.GetDirectionsPositions(pos, "4")
            .Where(next => int.Parse(map.GetValueOnMap(next).ToString()) ==
                           int.Parse(map.GetValueOnMap(pos).ToString()) + 1)
            .ToList();
}
