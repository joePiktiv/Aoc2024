
using Tools;

namespace Day16;

public class Maze
{
    public List<List<int[]>> Paths { get; set; }
    public Map Map { get; set; }
    
    public int[] Position { get; set; }
    public int[] End { get; set; }
    
    public int Steps { get; set; }

    public Maze(List<string> lines)
    {
        Map = new Map(lines);
        Position = Map.FindAll('S').First();
        End = Map.FindAll('E').First();
        var (steps, graph) = GetSteps();
        var q = new Queue<(int, int, int, string)>();
        q.Enqueue((steps,End[0], End[1], "down"));
        
        var dirs = new List<string> { "left", "up", "right", "down" };
        // var visited = new List<int[]> { End };
        var visited = new HashSet<(int, int)> { (End[0], End[1]) };
        while (q.Count > 0)
        {
            var current = q.Dequeue();
            var cost = current.Item1;
            var pos = new int[] { current.Item2, current.Item3 };
            var dir = current.Item4;
            
            if (Map.SamePosition(Position, pos))
            {
                Console.WriteLine($"Part 2 {visited.Count}");
            }
            // Console.WriteLine($"cost {cost} position [{pos[0]},{pos[1]}]");
            
            var stright = Map.GetDirectionsPositions(pos, ConvertDirectionToSign(dir, dirs));
            var left = Map.GetDirectionsPositions(pos, ConvertDirectionToSign(GetTurn("left", dir, dirs), dirs));
            var right = Map.GetDirectionsPositions(pos, ConvertDirectionToSign(GetTurn("right", dir, dirs), dirs));
            
            if (Goable(stright, visited)) 
            {
                var tuple = (cost - 1, stright[0][0], stright[0][1], dir);
                // var newPos = new [] {stright[0][0], stright[0][1]};
                // if (!graph.ContainsKey((newPos[0], newPos[1])) || graph[(newPos[0], newPos[1])] > cost)
                // {
                //     graph[(newPos[0], newPos[1])] = cost;
                //     q.Enqueue((cost, newPos[0], newPos[1], dir));
                //     visited.Add((newPos[0], newPos[1]));
                // }

                if (graph.ContainsKey((stright[0][0], stright[0][1])) &&
                    (graph[(stright[0][0], stright[0][1])] == cost - 1 ||
                     graph[(stright[0][0], stright[0][1])] == cost - 1001) )
                {
                    q.Enqueue( tuple);
                    visited.Add((stright[0][0],  stright[0][1]));
                }
            }
            if (Goable(left, visited))
            {
                var tuple = (cost - 1001, left[0][0], left[0][1], GetTurn("left", dir, dirs));
                if (graph.ContainsKey((left[0][0], left[0][1])) && (graph[(left[0][0], left[0][1])] == cost - 1001 || graph[(left[0][0], left[0][1])] == cost - 1) )
                    { 
                        q.Enqueue(tuple);
                        visited.Add((left[0][0], left[0][1]));
                    }
               
            }
            if (Goable(right, visited))
            {
                var tuple = (cost - 1001, right[0][0], right[0][1], GetTurn("right", dir, dirs));                
                if (graph.ContainsKey((right[0][0], right[0][1])) && (graph[(right[0][0], right[0][1])] == cost - 1001 || graph[(right[0][0], right[0][1])] == cost - 1 ) )
                    { 
                        q.Enqueue(tuple);
                        visited.Add((right[0][0], right[0][1]));
                    }
            }
        }
    }

    private (int, Dictionary<(int, int), int>) GetSteps()
    {
        var direction = "right";
        var dirs = new List<string> { "left", "up", "right", "down" };
        var queue = new PriorityQueue<(int, int, int, string), int>();
        var visited = new List<int[]> { Position };
        queue.Enqueue((0, Position[0], Position[1], direction), 0);
        var graph = new Dictionary<(int, int), int>();
        graph.Add((Position[0], Position[1]), 0);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var cost = current.Item1;
            var pos = new int[] { current.Item2, current.Item3 };
            var dir = current.Item4;
            
            if (Map.SamePosition(End, pos))
            {
                Console.WriteLine($"Part 1 {cost}");
                return (cost, graph);
            }
            //Console.WriteLine($"cost {cost} position [{pos[0]},{pos[1]}]");
            var stright = Map.GetDirectionsPositions(pos, ConvertDirectionToSign(dir, dirs));
            var left = Map.GetDirectionsPositions(pos, ConvertDirectionToSign(GetTurn("left", dir, dirs), dirs));
            var right = Map.GetDirectionsPositions(pos, ConvertDirectionToSign(GetTurn("right", dir, dirs), dirs));
            
            if (Goable(stright, visited)) 
            {
                var tuple = (cost + 1, stright[0][0], stright[0][1], dir);
                queue.Enqueue(tuple, cost + 1);
                visited.Add(stright[0]);
                graph.Add((tuple.Item2, tuple.Item3), tuple.Item1);
            }
            if (Goable(left, visited))
            {
                var tuple = (cost + 1001, left[0][0], left[0][1], GetTurn("left", dir, dirs));
                queue.Enqueue(tuple, cost + 1001);
                visited.Add(left[0]);
                graph.Add((tuple.Item2, tuple.Item3), tuple.Item1);
            }
            if (Goable(right, visited))
            {
                var tuple = (cost + 1001, right[0][0], right[0][1], GetTurn("right", dir, dirs));
                queue.Enqueue(tuple, cost + 1001);
                visited.Add(right[0]);
                graph.Add((tuple.Item2, tuple.Item3), tuple.Item1);
            }
            
        }

        return (-1, graph);
    }
    private bool Goable(List<int[]> pos, HashSet<(int, int)> visited)
    {
        if (pos.Count == 0)return false;
        if (!Map.OnBoard(pos[0])) return false;
        if (Map.GetValueOnMap(pos[0]) == '#') return false;
        if (IsVisited(visited, pos[0], Map)) return false;
        return true;
    }
    
    private bool Goable(List<int[]> pos, List<int[]> visited)
    {
        if (pos.Count == 0)return false;
        if (!Map.OnBoard(pos[0])) return false;
        if (Map.GetValueOnMap(pos[0]) == '#') return false;
        if (IsVisited(visited, pos[0], Map)) return false;
        return true;
    }

    private string? GetTurn(string change, string dir, List<string> dirs)
    {
        var index = dirs.IndexOf(dir);
        var left = (index + 3) % 4;
        var right = (index + 1) % 4;
        return change == "left" ? dirs[left] : dirs[right];
    }

    private string? ConvertDirectionToSign(string dir, List<string> dirs)
    {
        var index = dirs.IndexOf(dir);
        var signs = new List<string> { "+l", "+u", "+r", "+d" };
        //Console.WriteLine(dir);
        return signs[index];
    }
    


    private int Cost(List<int[]> path,int index)
    {
        // var prevPrev = path[0];
        // var prev = path[1];
        var cost = 1;
        for (int i = 1; i < path.Count; i++)
        {
            
            var prevPrev = i != 1 ? path[i-2] : new int[]{Map.FindAll('S').First()[0],0};
            var prev = path[i-1];
            var current = path[i];
            var direction1 = GetDirection(prevPrev, prev);
            var direction2 = GetDirection(prev, current) ;
            var turns = NumOfRightTurns(direction1, direction2);
            if (index == 5 || turns == 2) 
                Console.Write(turns +", ");
            cost += (turns == 1 || turns == 3 ? 1000 : turns == 2 ? 2000 : 0) + 1;
        }

        return cost - 1;
    }

    private int NumOfRightTurns(string dir1, string dir2)
    {
        var list = new List<string> { "up", "right", "down", "left" };
        var dir1Index = list.IndexOf(dir1);
        var dir2Index = list.IndexOf(dir2);
        return (dir2Index + 4 - dir1Index) % 4;
    }

    private string GetDirection(int[] pos1, int[] pos2)
    {
        if (pos1[0] == pos2[0])
        {
            // left or right
            return (pos2[1] > pos1[1]) ? "right" : "left";

        }
        else
        {
            // up or down
            return pos2[0] > pos1[0] ? "down" : "up";
        }
        
    }

    private List<List<int[]>> Move()
    {
        var newPaths = new List<List<int[]>>();
        foreach (var path in Paths)
        {
            if (Map.SamePosition(End, path.Last()))
            {
                newPaths.Add(path);
                continue;
            }
            var last = path.Last();
            if (last[0] == 1 && last[1] == 1)
                Console.WriteLine("position "+last[0]+','+last[1]);
            var fourDirections = Map.GetDirectionsPositions(last, "4");
            var availables = fourDirections.Where(d => Map.OnBoard(d) && !IsWall(Map, d) && !IsVisited(path, d, Map)).ToList();
            newPaths.AddRange( availables.Select(a => path.Concat(new List<int[]>{a}).ToList()).ToList());
        }

        return newPaths;

    }

    private bool IsWall(Map map, int[] pos)
    {
        return map.GetValueOnMap(pos) == '#';
    }
    private bool IsVisited(List<int[]> path, int[] pos, Map map)
    {
        return path.Any(p => Map.SamePosition(pos, p));
    }
    private bool IsVisited(HashSet<(int, int)> path, int[] pos, Map map)
    {
        return path.Any(p => Map.SamePosition(pos, new []{p.Item1, p.Item2}));
    }
    
}