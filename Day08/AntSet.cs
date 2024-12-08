using System.ComponentModel.Design;
using Tools;

namespace Day08;

public class AntSet
{
    public Map Map { get; set; }
    public List<int[]> Set { get; set; }
    public List<int[]> Antinodes { get; set; }
    
    public List<int[]> UpdatedAntinodes { get; set; }
    public AntSet(List<int[]> set, Map map)
    {
        Map = map;
        Set = set;
        (Antinodes, UpdatedAntinodes) = GetAntinodoes();
    }

    private (List<int[]>, List<int[]>) GetAntinodoes()
    {
        var og = new List<int[]>();
        var adv = new List<int[]>();
        for (int i = 0; i < Set.Count-1; i++)
        {
            var first = Set[i];
            for (int j = i+1; j < Set.Count; j++)
            {
                var second = Set[j];
                var rDiff = first[0] - second[0];
                var cDiff = first[1] - second[1];
                var listPos = GetPoints(first, second, rDiff,cDiff);
                var ListPosAdvance = GetPointsAdvance(first, second, rDiff,cDiff, Map);
                og.AddRange(listPos.Where(p => Map.OnBoard(p)).ToList());
                adv.AddRange(ListPosAdvance.Where(p => Map.OnBoard(p)).ToList());
            }   
        }
        
        return (Map.RemoveDuplicates(og), Map.RemoveDuplicates(adv));
    }

    private List<int[]> GetPointsAdvance(int[] first, int[] second, int rDiff, int cDiff, Map map)
    {
        var result = new List<int[]>{};
        var pointSet = new List<int[]>{first, second};
        while (AtLeaseOneOnBoard(pointSet))
        {
            result.AddRange(pointSet);
            pointSet = GetPoints(pointSet[0], pointSet[1], rDiff, cDiff);
        }
        return result;
    }

    private bool AtLeaseOneOnBoard(List<int[]> pointSet)
    {
        return pointSet.Any(p => Map.OnBoard(p));
    }

    private List<int[]> GetPoints(int[] first, int[] second, int rDiff, int cDiff)
    {
        
        var poi1 = new int[] {first[0] + rDiff, first[1] + cDiff }; 
        var poi2 = new int[] {second[0] -rDiff, second[1] -cDiff };
        return new List<int[]>() { poi1, poi2 };
    }

   
}