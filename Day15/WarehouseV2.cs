using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text;
using Tools;

namespace Day15;

public class WarehouseV2
{
    Map Map { get; set; }
    string Moves { get; set; }
    int[] Robot { get; set; }
    public List<int[]> Walls { get; set; }
    string Box { get; set; }

    public WarehouseV2(List<string> lines)
    {
        var split = string.Join(Environment.NewLine, lines).Split(new string[]{ "\r\n\r\n" },
        StringSplitOptions.RemoveEmptyEntries);
        Map = new Map(ScaleUp(split[0]).Split(Environment.NewLine).ToList());
        Moves = split[1].Replace("\r\n", "");
        Robot = Map.FindAll('@').First();
        Walls = Map.FindAll('#');
        Box = "[]";
        do
        {
            Move();
            Robot = Map.FindAll('@').First();
            
        }
        while (Moves.Length > 0);
        // pick up the first in move , moveList is the rest of the moves
        // identify the move sign then move the move accordingly 
        // either row or col, if there is space then change the row or col layout on may and all variables above.
        var posOfOs = Map.FindAll('[');
        var sum =posOfOs.Select(p => p[0] * 100 + p[1]).Sum();
        Console.WriteLine("Part 2 sum is "+sum);

    }

    private string ScaleUp(string s)
    {
        var old = new List<char>() { '#', 'O', '.', '@' };
        var replacement = new List<string>() { "##", "[]", "..", "@." };
        var str = "";
        foreach (var oldChar in s)
        {
            var index = old.IndexOf(oldChar);
            if (index != -1)
                str += replacement[index];
            else str += oldChar;
        }

        return str;
    }

    public void Move()
    {
        if (Moves.Length == 0) return;
        var next = Moves[0];
        Moves = Moves.Substring(1);
        var line = (next == '^' || next == 'v') ? Map.GetColValues(Robot[1]) : Map.GetRowValues(Robot[0]); 
        var indexRobot = line.IndexOf('@');
        List<char> str;
        if (next == '<' || next == '>')
        {

            if (next == '^' || next == '<')
            {
                str = line.Slice(0, indexRobot + 1);
                str.Reverse();
            }
            else str = line.Slice(indexRobot, line.Count - indexRobot);

            var firstWall = str.IndexOf('#');
            var beforeFirstWall = str.Slice(0, firstWall);
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
                // replace col Robot[1]
                Map = ReplaceColOnMap(Map, Robot[1], newLine);
            }
            else
            {
                // replace row Robot[0]
                string ny = new string(newLine.ToArray());
                Map.Lines[Robot[0]] = ny;
            }
        }

        // handle up and down functions
        if (next == '^' || next == 'v')
        {
            List<int[]> outerLayerPos = new List<int[]>{Robot};
            List<char> outerLayerValue;
            List<int[]> containerBox = new List<int[]>{};
            do
            {
                containerBox = containerBox.Concat(outerLayerPos).ToList();
                (outerLayerValue, outerLayerPos) = GetAllOutterBoxLayerPositions(Map, outerLayerPos , next == '^' ? "up" : "down" );
            } while (outerLayerValue.Any(o => o == Box[0] || o == Box[1]));

            containerBox = Map.RemoveDuplicates(containerBox).Where(c => !outerLayerPos.Any(o => o[0] == c[0] && o[1] == c[1])).ToList();
            
            outerLayerPos = Map.RemoveDuplicates(outerLayerPos);
            outerLayerPos = ProcessOutLayer(outerLayerPos, next == '^' ? "up" : "down");
            
            // if outer layer contains wall do nothing
            if (!outerLayerValue.Contains('#'))
                // if outer layer all "." move all dot to col lowest positions;
            {
                outerLayerPos.ForEach(o => SwapPositions(o, containerBox, Map, next == '^' ? "up" : "down"));
            }
        } 

    }

    private List<int[]>? ProcessOutLayer(List<int[]>? outerLayerPos, string indicator)
    {
        var pos = outerLayerPos.GroupBy(pos => pos[1]) // Group by column number (pos[1])
            .Select(group =>
            {
                if (indicator == "up")
                {
                    // Pick the position with the smallest row number in the group
                    return group.OrderBy(pos => pos[0]).First();
                }
                else // "down"
                {
                    // Pick the position with the largest row number in the group
                    return group.OrderByDescending(pos => pos[0]).First();
                }
            })
            .ToList();
        return pos;
    }


    private Map ReplaceColOnMap(Map map, int col, List<char> newLine)
    {
        
        for (int i = 0; i < newLine.Count; i++)
        {
            var sb = new StringBuilder(map.Lines[i]);
            sb[col] = newLine[i];
            map.Lines[i] = sb.ToString();
        }

        return map;
    }

    private void SwapPositions(int[] pos, List<int[]> containerBox, Map map, string indicator)
    {
        var col = pos[1];
        var oldLine = map.GetColValues(col);
        var selected = containerBox.Where(c => c[1] == col).ToList();
        var (heads, ends) = GetAllLast(selected, "up");
        
        var newCol = new StringBuilder(new string(oldLine.ToArray()));
        if (indicator == "up")
        {
            for (int i = 0; i < heads.Count; i++)
            {
                var head = heads[i];
                var end = ends[i];
                for (int j = head - 1; j < end; j++)
                {
                    newCol[j] = oldLine[j + 1];
                }

                newCol[end] = '.';
            }
            
        }
        else
        {
            for (int i = 0; i < heads.Count; i++)
            {
                var head = heads[i];
                var end = ends[i];
                for (int j = end + 1; j > head; j--)
                {
                    newCol[j] = oldLine[j - 1];
                }

                newCol[head] = '.';
            }
        }
            
        
        Map = ReplaceColOnMap(map, pos[1], newCol.ToString().ToCharArray().ToList());

    }

    private (List<int>, List<int>) GetAllLast(List<int[]> selected, string indicator)
    {
        var numbers = selected.Select(s => s[0]).ToList();
        numbers.Sort();
        var firstOnes = new List<int>();
        var lastOnes = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                // Check if it's the start of a new range
                if (i == 0 || numbers[i] != numbers[i - 1] + 1)
                {
                    firstOnes.Add(numbers[i]); // Add to the list of first elements
                }

                // Check if it's the end of a range
                if (i == numbers.Count - 1 || numbers[i] != numbers[i + 1] - 1)
                {
                    lastOnes.Add(numbers[i]); // Add to the list of last elements
                }
            }

            return (indicator == "up" ? firstOnes : lastOnes, indicator == "up" ? lastOnes : firstOnes);

    }

    private (List<char> outerLayerValue, List<int[]> outerLayerPos) GetAllOutterBoxLayerPositions(Map map, List<int[]> poses, string direction)
    {
        var notBoxNotRobot = poses.Where(p => !"@[]".Contains(map.GetValueOnMap(p))).ToList();
        var boxOrRobot = poses.Where(p => "@[]".Contains(map.GetValueOnMap(p))).ToList();
        var upps = boxOrRobot.Select(p => map.GetDirectionsPositions(p, direction == "up" ?"+u":"+d").First()).ToList();
        var nextBoxes = upps.SelectMany(p => GetBox(map, p, Box))
            .ToList();
        var pos = notBoxNotRobot.Concat(nextBoxes).ToList() ;
        var val = pos.Select(p => map.GetValueOnMap(p)).ToList();
        return (val, pos);
    }

    private List<int[]> GetBox(Map map, int[] pos, string box)
    {
        var value = map.GetValueOnMap(pos);
        var leftOrRight = box.IndexOf(value);
        var isBox = leftOrRight == -1 ? false : true;
        return (isBox ? new List<int[]> { pos, new int[] { pos[0], pos[1] + (leftOrRight == 0 ? +1 : -1) } } : new List<int[]> { pos });
    }
}