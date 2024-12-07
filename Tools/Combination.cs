namespace Tools;

public class Combination
{
    public static List<List<string>> GenerateCombinations(int length, List<string> elements)
    {
        var result = new List<List<string>>();
        GenerateCombinationsRecursive(new List<string>(), length, result, elements);
        return result;
    }

    private static void GenerateCombinationsRecursive(List<string> current, int remaining, List<List<string>> result, List<string> elements)
    {
        if (remaining == 0)
        {
            result.Add(new List<string>(current)); // Add a copy of current to the result
            return;
        }

        foreach (var element in elements)
        {
            current.Add(element);
            GenerateCombinationsRecursive(current, remaining - 1, result, elements);
            current.RemoveAt(current.Count - 1); // Backtrack
        }
    }
}