namespace Tools;

public class Combination
{
    public static List<List<T>> GetPermutations<T>(List<T> list)
    {
        // Base case: if the list is empty, return a list with an empty list
        if (list.Count == 0)
        {
            return new List<List<T>> { new List<T>() };
        }

        var permutations = new List<List<T>>();

        for (int i = 0; i < list.Count; i++)
        {
            // Pick the current element
            T currentElement = list[i];

            // Create a sublist excluding the current element
            var remainingList = new List<T>(list);
            remainingList.RemoveAt(i);

            // Get permutations of the remaining list recursively
            var remainingPermutations = GetPermutations(remainingList);

            // Prepend the current element to each of these permutations
            foreach (var perm in remainingPermutations)
            {
                var newPermutation = new List<T> { currentElement };
                newPermutation.AddRange(perm);
                permutations.Add(newPermutation);
            }
        }
        return permutations;
    }
    public static List<List<string>> GetCombinations(int length, List<string> elements)
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