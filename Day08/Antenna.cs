using Tools;

namespace Day08;

public class Antenna
{
    public List<AntSet> FrequenciesList { get; set; } 
    public string Frequencies { get; set; }
    public Antenna(Map map)
    {
        (Frequencies, FrequenciesList) = GetFrequencies(map);
    }

    private (string, List<AntSet>?) GetFrequencies(Map map)
    {
        var exception = "#.";
        var frequencies = "";
        var frequenciesList = new List<List<int[]>>();
        for (var r =0; r<map.Row; r++)
        {
            for (var c = 0; c < map.Col; c++)
            {
                var point = map.GetValueOnMap(r, c);
                if (!exception.Contains(point))
                {
                    if (!ExistingFrequency(frequencies, point))
                    {
                        // add to frequencies
                        frequencies += point;
                        // add to Frequencies List
                        frequenciesList.Add(new List<int[]>{new int[] {r,c}});
                    }
                    else
                    {
                        var pos = frequencies.IndexOf(point);
                        frequenciesList[pos].Add(new int[]{r,c});
                    }
                    
                }
            }
        }

        return (frequencies, frequenciesList.Select(f => new AntSet(f,map)).ToList());
    }

    private bool ExistingFrequency(string frequencies, char point)
    {
        return frequencies.Contains(point);
    }
}