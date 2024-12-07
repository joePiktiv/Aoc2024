namespace Day02;

public class Report
{
    public List<int> Values { get; set; }
    public List<int> Increments { get; set; }
    public bool IsSafe { get; set; }
    public bool BliSafe { get; set; }
    public bool CheckSafe () 
    {
        var sameSign = Increments.All(i => i > 0) || Increments.All(i => i < 0);
        var inRange = Increments.All(i => Math.Abs (i) <=3 && Math.Abs (i) > 0);
        return sameSign && inRange;
    }
    public Report(string line, bool keepReduce)
    {
        var splits = line.Split(' ');
        Values = new List<int>{int.Parse(splits[0])};
        Increments = new List<int>();
        for (var i = 1; i < splits.Length; i++)
        {
            var current = int.Parse(splits[i]);
            var prev = Values[i - 1];
            Values.Add(current);
            Increments.Add(current - prev);
        }

        IsSafe = CheckSafe();

        if (keepReduce)
            BliSafe = IsSafe || Values.Select((v,i) => {
                var newValues = Values.Where((s,index) => index != i).ToList();
                var newLine = string.Join(' ', newValues);
                var newReport = new Report(newLine, false);
                return newReport.IsSafe;
            } ).Any(a => a);
    }
    
}