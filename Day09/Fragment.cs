namespace Day09;

public class Fragment
{ 
    public int Start { get; set; }
    public short Id { get; set; }
    public List<short> Value { get; set; }
    public short Empty { get; set; }

    public Fragment(int start, string code, short id)
    {
        Start = start;
        Id = id;
        var num = short.Parse(code[0].ToString());
        var space = (short) code.Length == 1 ? 0 : short.Parse(code[1].ToString());
        Value = Enumerable.Range(0, num).Select(i => Id).ToList();
        Empty = (short) space;
    }

    public Fragment(int start, short length, short id, short empty)
    {
        Start = start;
        Id = id;
        Value = Enumerable.Range(0, length).Select(i => Id).ToList();
        Empty = empty;
    }

    public long GetCheckSum()
    {
        var start = Start;
        long sum = 0;
        for (int i = 0; i < Value.Count; i++)
        {
            sum += start * Id;
            start++;
        }
        return sum;
    }
}