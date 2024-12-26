
using Tools;

namespace Day18;

public class Run
{
    public Map Map { get; set; }

    public int[] Start { get; set; }

    public int[] End { get; set; }
    public int Step { get; set; }

    public Run(List<string> lines, int row, int col, int counter)
    {
        Map = new Map(CreateMapDesign(lines, row, col, counter));
        Start = new int[] { 0, 0 };
        End = new int[] { row - 1, col - 1 };
        Step = FindStepsToEnd();
    }

    public int FindStepsToEnd()
    {
        var queue = new Queue<(int, int, int)>();
        queue.Enqueue((0, Start[0], Start[1]));
        var visited = new HashSet<(int, int)> { (Start[0], Start[1]) };
        var parents = new Dictionary<(int, int), int>();
        parents.Add(visited.First(), 0);
        while (queue.Count > 0)
        {
            var (value, r, c) = queue.Dequeue();
            var nexts = new HashSet<(int, int)> { (r - 1, c), (r, c + 1), (r + 1, c), (r, c - 1) };
            var movable = nexts.Where(x => Movable(Map, visited, x.Item1, x.Item2)).ToList();
            if (movable.Contains((End[0], End[1])))
                return value + 1;
            movable.ForEach(m => queue.Enqueue((value + 1, m.Item1, m.Item2)));
            movable.ForEach(m => visited.Add((m.Item1, m.Item2)));
        }
        return -1;
    }

    private bool Movable(Map map, HashSet<(int, int)> visited, int row, int col)
    {
        return !Visited(visited, row, col) && OnBoard(map, row, col) && !IsWall(map, row, col);
    }

    private bool Visited(HashSet<(int, int)> visited, int row, int col)
    {
        return visited.Contains((row, col));
    }

    private bool OnBoard(Map map, int row, int col)
    {
        return (row >= 0 && col >= 0 && row < map.Row && col < map.Col);
    }

    private bool IsWall(Map map, int row, int col)
    {
        if (OnBoard(map, row, col))
            return (map.GetValueOnMap(row, col) == '#');
        return false;
    }

    public List<string> CreateMapDesign(List<string> lines, int row, int col, int counter)
    {
        var bytes = lines
            .Select(l =>
            {
                var split = l.Split(',');
                var c = int.Parse(split[0]);
                var r = int.Parse(split[1]);
                return new int[] { r, c };
            })
            .Where((x, index) => index < counter)
            .ToList();
        var newLines = new List<string>();
        for (int i = 0; i < row; i++)
        {
            var line = "";
            for (int j = 0; j < col; j++)
            {
                line += bytes.Any(b => b[0] == i && b[1] == j) ? "#" : '.';
            }
            newLines.Add(line);
        }
        return newLines;
    }
}
