namespace Day24;

public class Wire
{
    public static (string, int) GetWire(string line, Dictionary<string, int> dic)
    {
        var (desKey, key1, oper, key2) = ReadLine(line);
        var v1 = dic[key1];
        var v2 = dic[key2];
        var desVal = Oper(v1, v2, oper);
        return (desKey, desVal);
    }

    public static (string desKey, string key1, string oper, string key2) ReadLine(string line)
    {
        var split = line.Split(" -> ");
        var desKey = split[1];
        var inputSplit = split[0].Split(' ');
        var key1 = inputSplit[0];
        var oper = inputSplit[1];
        var key2 = inputSplit[2];
        return (desKey, key1, oper, key2);
    }


    private static int Oper(int v1, int v2, string oper)
    {
        var v1Bool = v1 == 1 ? true : false;
        var v2Bool = v2 == 1 ? true : false;
        if (oper == "AND")
        {
            return v1Bool && v2Bool ? 1 : 0;
        }
        if (oper == "OR")
        {
            return v1Bool || v2Bool ? 1 : 0;
        }
        if (oper == "XOR")
        {
            return v1Bool ^ v2Bool ? 1 : 0;
        }
        Console.WriteLine("Error ");
        return -1;
    }

    public static bool TryGetWire(string line, Dictionary<string, int> dic)
    {
        var (desKey, key1, oper, key2) = ReadLine(line);
        return dic.ContainsKey(key1) && dic.ContainsKey(key2);
    }

    public static string ChangeDeskey(string line, string dk)
    {
        var (desKey, key1, oper, key2) = ReadLine(line);
        return $"{key1} {oper} {key2} -> {dk}";
    }
}