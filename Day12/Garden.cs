using Tools;

namespace Day12;

public class Garden
{
    public Map Map { get; set; }
    public int Groups { get; set; } 
    public List<Cell> Cells { get; set; }
    public int CostsByLengths { get; set; }
    public List<int> Sizes { get; set; }
    public Garden(List<string> lines)
    {
        Map = new Map(lines);
        Cells = new List<Cell>();
        for (int i = 0; i < Map.Row ; i++)
        {
            for (int j = 0; j < Map.Col; j++)
            {
                var pos = new int[] {i, j};
                var cell = new Cell( pos, Map);
                Cells.Add(cell);
            }
        }

        Groups = FormGroups();
        CostsByLengths = GetCosts();
        Sizes = GetSizes();
    }

    private List<int> GetSizes()
    {
        var result = new List<int>();
        for (int id = 1; id <= Groups; id++)
        {
            var cells = Cells.Where(c => c.GroupId == id).ToList();
            var numCels = cells.Count;
            var outerCorners = cells.Sum(c => c.GetOuterCorners(Map));
            var innerCorners = GetInnerCorners(cells, cells[0].Char, id);
            var corners = innerCorners + outerCorners;
            result.Add(numCels * corners);
        }

        return result;
    }

    private int GetInnerCorners(List<Cell> cells, char ch, int id)
    {
        var sum = 0;
        var minR = cells.Min(c => c.Position[0]);
        var maxR = cells.Max(c => c.Position[0]);
        var minC = cells.Min(c => c.Position[1]);
        var maxC = cells.Max(c => c.Position[1]);
        for (int r = minR; r <= maxR; r++)
        {
            for (int c = minC; c <= maxC; c++)
            {
                sum += GetInnerCorners(Map, new int[] { r, c }, ch, id);
            }
        }
        return sum;
    }
    
    private int GetInnerCorners(Map map, int[] pos, char ch, int id)
    {
        var square = map.GetDirectionsPositions(pos, "+r+d-dr");
        square.Add(pos);
        var cells = Cells.Where(c =>
            square.Any(a => a[0] == c.Position[0] && a[1] == c.Position[1])).ToList();
        var withSameId = cells.Count(c => c.GroupId == id);
        return withSameId == 3 ? 1 : 0;
    }

    private int GetCosts()
    {
        var cost = 0;
        for (int i = 1; i <= Groups; i++)
        {
            var cells = Cells.Where(c => c.GroupId == i).ToList();
            var fences = cells.Sum(c => c.Fence);
            var size = cells.Count;
            cost += fences * size;
        }
        return cost;
    }

    private int FormGroups()
    {
        var counter = 0;
        foreach (var cell in Cells)
        {
            if (cell.GroupId != -1) continue;
            counter++;
            GroupAccessableSameSignNeighbours(cell, counter);
        }
        return counter ;
    }

    private void GroupAccessableSameSignNeighbours(Cell cell, int counter)
    {
        cell.GroupId = counter;
        var neighbours = Map.GetDirectionsPositions(cell.Position, "4");
        var accessible = neighbours.Where(n => Map.GetValueOnMap(n) == cell.Char).ToList();
        var unGroupedAccessables = Cells.Where(c =>
            accessible.Any(a => a[0] == c.Position[0] && a[1] == c.Position[1]) && c.GroupId == -1).ToList();
        foreach (var c in unGroupedAccessables)
            GroupAccessableSameSignNeighbours(c, counter);
    }
}