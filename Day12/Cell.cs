using Tools;

namespace Day12;

public class Cell
{
    public int[] Position { get; set; }
    public char Char { get; set; }
    public int GroupId { get; set; }
    public int Fence { get; set; }

    public Cell(int[] pos, Map map)
    {
        Position = pos;
        Char = map.GetValueOnMap(pos);
        GroupId = -1;
        var neighbours = map.GetDirectionsValues(pos, "4");
        Fence = GetNumFences(neighbours, Char);
    }

    private int GetNumFences(List<char> neighbours, char c)
    {
        var boarders = 4 - neighbours.Count;
        var differences = neighbours.Count(n => n != c);
        return boarders + differences;
    }
    
    public int GetOuterCorners(Map map)
    {
        var upChar = map.GetDirectionsValues(Position, "+u");
        var leftChar = map.GetDirectionsValues(Position, "+l");
        var rightChar = map.GetDirectionsValues(Position, "+r");
        var downChar = map.GetDirectionsValues(Position, "+d");
        
        var boundUp = upChar.Count == 0 || upChar[0] != Char; 
        var boundLeft = leftChar.Count == 0 || leftChar[0] != Char; 
        var boundRight = rightChar.Count == 0 || rightChar[0] != Char; 
        var boundDown = downChar.Count == 0 || downChar[0] != Char;
        
        var count = boundUp && boundLeft == true ? 1 :0;
        count += boundDown && boundLeft == true ? 1 :0;
        count += boundUp && boundRight == true ? 1 :0;
        count += boundDown && boundRight == true ? 1 :0;
        return count;
    }
}