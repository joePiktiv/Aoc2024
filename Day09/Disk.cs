
namespace Day09;

public class Disk
{
    public string Line { get; set; }
    public int Length { get; set; }
    public int ValuesLength { get; set; }
    public List<Fragment> Fragments { get; set; } 
    public List<Fragment> List { get; set; }

    public Disk(string line, int type)
    {
        Length = line.Sum(c => int.Parse(c.ToString()));
        ValuesLength = line.Where((c, index) => index % 2 == 0).Sum(c => int.Parse(c.ToString()));
        Line = line;
        Fragments = GetFragments()!;
        List = type == 1 ? ReverOrderFill() : FillSpacesFillx();
    }

    private List<Fragment> FillSpacesFillx()
    {
        var head = new List<Fragment>();
        var restList = Fragments.Skip(0).ToList(); 
        while (restList.Count > 0 || restList.Sum(r => r.Value.Count) > 0)
        {
            var fragment = restList.First();
            restList.RemoveAt(0);
            var space = fragment.Empty;
            var needFragments = new List<Fragment>();
            var searching = true;
            while (space > 0 && restList.Any() && searching)
            {
                    var ( element,  index, found) = FindTarget(restList, space);
                    if (found)
                    {
                        space = (short)(space - (short) element!.Value.Count);
                        restList = restList.Select((e, i) =>
                        {
                            if (i != index) return e;
                            var newElement = new Fragment(e.Start, (short) e.Value.Count, 0, (short) e.Empty );
                            return newElement;
                        }).ToList();
                        needFragments.Add(element);
                    } else searching = false;
            }
            if (space > 0)
            {
                var lastInNeeded = new Fragment(-1, space, 0, 0);
                needFragments.Add(lastInNeeded);
            }
            fragment.Empty = 0;
            head.Add(fragment);
            foreach (var f in needFragments)
            {
                var start = head.Sum(h => h.Value.Count);
                var newF = new Fragment( start, (short) f.Value.Count, f.Id, 0);
                head.Add(newF);
            }
        }
        return head;
    }

    private static (Fragment? last, int index, bool found) FindTarget(List<Fragment> restList, short target)
    {
        for (int i = restList.Count-1; i > -1; i--)
        {
            var current = restList[i];
            if (current.Id == 0 || current.Value.Count == 0) continue;
            if (current.Value.Count <= target)
                return (current, i, true);
        }

        return (null, -1, false);

    }
    private List<Fragment> ReverOrderFill()
    {
        var head = new List<Fragment>();
        var restList = Fragments.ToList();

        while (restList.Any())
        {
            var fragment = restList.First();
            restList.RemoveAt(0);
            var space = fragment.Empty;
            var needFragments = new List<Fragment>();

            while (space > 0 && restList.Any())
            {
                var last = restList.Last();
                restList.RemoveAt(restList.Count - 1);
                space -= (short)last.Value.Count;
                needFragments.Add(last);
            }

            if (space < 0)
            {
                var numRemain = -space;
                var last = needFragments[^1];
                var lastInNeeded = new Fragment(last.Start, (short)(last.Value.Count - numRemain), last.Id, 0);
                var remain = new Fragment(last.Start + lastInNeeded.Value.Count, (short)numRemain, last.Id, 0);
                needFragments[^1] = lastInNeeded;
                restList.Add(remain);
            }

            fragment.Empty = 0;
            head.Add(fragment);

            foreach (var f in needFragments)
            {
                var start = head.Sum(h => h.Value.Count);
                head.Add(new Fragment(start, (short)f.Value.Count, f.Id, 0));
            }
        }

        return head;
    }

    private List<Fragment>? GetFragments()
    {
        var result = new List<Fragment>();
        for (int i = 0, start = 0; i <= Line.Length ; i+=2)
        {
            var code = Line.Substring(i, Math.Min(2, Line.Length - i));
            var f = new Fragment( start, code,(short)(i / 2));
            result.Add(f);
            start += f.Value.Count + f.Empty;
        }
        return result;
    }
}

