using System.Text;
using Tools;

namespace Day06;

public class Guard
{
    public int[] Position { get; set; }
    public int[] Start { get; set; }
    public string Direction { get; set; }
    public Map Map { get; set; }
    public List<int[]> Path { get; set; }
    public List<char> Signs { get; set; }
    public List<int[]> Distincts { get; set; }
    public bool IsLoop { get; set; }
    public bool GoLower { get; set; }
    public List<int[]> Obses { get; set; }
    public Guard (Map map, bool goLower)
    {
        GoLower = goLower;
        Map = map;
        Signs = new List<char> { '>', '<', '^', 'v' }  ;
        var (position, sign) = FindGuard(map);
        Position = position;
        Start = position;
        Direction = GetStartingDirection(sign);
        Path = new List<int[]>(){Position};
        IsLoop = false;
        Move();
        Distincts = GetDistinctPos(Path);
        Obses = PlaceObs();
    }

    public List<int[]> PlaceObs()
    {
        if (GoLower == false) return new List<int[]>();
        var counter = 0;
        var obses = new List<int[]>();
        foreach (var pos in Distincts)
        {
            if (pos[0] == Start[0] && pos[1] == Start[1]) continue;
            var map = AddObsOnMap(pos);
            var guard = new Guard(map, false);
            counter += guard.IsLoop ? 1 : 0;
            if (guard.IsLoop)
            {
                obses.Add(pos);
            }
        }

        return obses;
    }

    public Map AddObsOnMap(int[] pos)
    {
        var row = Map.Lines[pos[0]];
        var newRow = new StringBuilder(row);
        newRow[pos[1]] = '#';
        var newLines = Map.Lines.Select((l, index) => index != pos[0] ? l : newRow.ToString()).ToList();
        return new Map(newLines);
    }
    
    public List<int[]> GetDistinctPos(List<int[]> path)
    {
        var newList = new List<int[]>();
        foreach (var pos in path)
        {
            if (newList.Any(p => p[0] == pos[0] && pos[1] == p[1])) continue;
            newList.Add(pos);
        }
        return newList;
    }

    public void Move()
    {
        while (Map.OnBoard(Position) && !IsLoop)
        {
            var (nextChar, nextPositon) = NextPosition();
            
            // find the char,
            if (nextChar == '#')
            {
                // if it is # move right of current direction
                Direction = ChangeDirection(Direction, "right");
            }
            // update direction 
            else
            {
                // update position
                Position = nextPositon;
                // update path
                if (Map.OnBoard(Position)) Path.Add(Position);
            } 
            var indexesOfPos = FindRepeatPos(Path, Position);
            if (indexesOfPos.Count >= 3)
            {
                IsLoop = IsItALoop(Path,indexesOfPos);
            }
        }
    }

    private bool IsItALoop(List<int[]> path, List<int> indexes)
    {
        var lastEnd = indexes[indexes.Count - 1];
        var lastStart = indexes[indexes.Count - 2];
        var firstStart = indexes[indexes.Count - 3];
        var secondLength = lastEnd - lastStart;
        var firstLength = lastStart - firstStart;
        if (secondLength != firstLength) return false;
        var secondRun = path.Where((pos, index) => index >= lastStart).ToList();
        var firstRun  = path.Where((pos, index) => index >= firstStart && index <= lastStart).ToList();
        return CheckSame(firstRun, secondRun);
    }

    private bool CheckSame(List<int[]> firstRun, List<int[]> secondRun)
    {
        for (int i = 0; i < firstRun.Count; i++)
        {
            var first = firstRun[i];
            var second = secondRun[i];
            if (!(first[0] == second[0] && first[1] == second[1])) return false;
        }
        return true;
    }

    private List<int> FindRepeatPos(List<int[]> path, int[] position)
    {
        var result = new List<int>();
        for (int i = 0; i < path.Count; i++)
        {
            var current = path[i];
            if (position[0] == current[0] && position[1] == current[1]) result.Add(i);
        }
        return result;
    }

    private string ChangeDirection(string direction, string move)
    {
        var directions = new List<string> { "right", "down", "left", "up" };
        var currentDirection = directions.IndexOf(direction);
        var changes = 0;
        switch (move)
        {
            case "right" :
                changes = 1;
                break;
            case "down" :
                changes = 2;
                break;
            case "left" :
                changes = 3;
                break;
        }

        return directions[(currentDirection + changes) % directions.Count];
    }

    public (char, int[]) NextPosition()
    {
        var value = '-';
        var position = new int[] {};
        // Console.WriteLine("Direction "+Direction);
        switch (Direction)
        {
            case "right":
                position = new int[] { Position[0], Position[1]+1};
                value = Map.OnBoard(position) ? Map.GetValueOnMap(position[0], position[1]) : '-';
                break;
            case "left":
                position = new int[]{ Position[0], Position[1]-1};
                value = Map.OnBoard(position) ? Map.GetValueOnMap(position[0], position[1]) : '-';
                break;
            case "up":
                position = new int[]{ Position[0] -1, Position[1]};
                value = Map.OnBoard(position) ? Map.GetValueOnMap(position[0], position[1]) : '-';
                break;
            case "down":
                position = new int[] { Position[0] + 1, Position[1]};
                value = Map.OnBoard(position) ? Map.GetValueOnMap(position[0], position[1]) : '-';
                break;
            
            default:
                break;
        }
        return (value, position) ;
    }

    public string GetStartingDirection (char sign) 
    {
        var directions = new List<string> { "right","left", "up", "down"};
        var index = Signs.IndexOf(sign);
        return directions[index];
    }
    public (int[], char) FindGuard (Map map)
    {
        var lines = map.Lines;
        var signs = new List<char> { '>', '<', '^', 'v' }  ;
        for (int i = 0; i < lines.Count; i++)
        {
            //var line = lines[i];
            for (int j = 0; j< lines[i].Length; j++)
            {
                var sign = map.GetValueOnMap(i,j);
                if (signs.Contains(sign)) return (new int[] {i,j}, sign) ;
            }
        } 
        return (new int[] {-1 , -1},'x');
    }
    
}