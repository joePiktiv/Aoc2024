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

    public char GetValueOnMap (int r, int c)
    {
        return Lines[r][c];
    }

    public bool OnBoard (int[] p)
    {
        var r = p[0];
        var c = p[1];
        return ((r>=0 && r<Row) && (c<Col && c >= 0));
    }
    
}