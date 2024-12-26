
namespace Day17;

public class Computer
{
    public int A { get; set; }
    public int B { get; set; }
    public int C { get; set; }
    public List<int> Operations { get; set; }

    public List<int> Output { get; set; }

    public Computer(List<string> lines)
    {
        Output = new List<int>();
        foreach (var line in lines)
        {
            if (line.StartsWith("Register"))
            {
                var split = line.Split(": ");
                var key = split[0].Split(' ')[1];
                var value = int.Parse(split[1]);
                switch (key)
                {
                    case "A":
                        A = value;
                        break;
                    case "B":
                        B = value;
                        break;
                    case "C":
                        C = value;
                        break;
                }
            }
            if (line.StartsWith("Program"))
            {
                Operations = line.Split(' ')[1].Split(',').Select(s => int.Parse(s)).ToList();
            }
        }
        var pointer = 0;
        do
        {
            pointer = new Operation(Operations[pointer], Operations[pointer + 1], this).Evaluate(
                this,
                pointer
            );
        } while (pointer != Operations.Count());
    }

    public override string ToString()
    {
        var str = $"Register A: {A} {Environment.NewLine}";
        str += $"Register B: {B} {Environment.NewLine}";
        str += $"Register C: {C} {Environment.NewLine} {Environment.NewLine}";
        str += $"Output: {string.Join(',', Output)} {Environment.NewLine}";
        return str;
    }
    
    public ulong ReverseEng()
    {
        var program = new[] {2, 4, 1, 7, 7, 5, 1, 7, 4, 6, 0, 3, 5, 5, 3, 0};
        ulong start = 8;
        var output = new List<int>();
        
        while (true) 
        {
            var value = Try(start++);
            if (value != 0) return value;
        }

        ulong Try(ulong toTry)
        {
            Run(toTry);

            // Are the lists output and program the same?
            if (output.Count == program.Length)
            {
                var same = true;
                for (var i = 0; i < program.Length; i++)
                {
                    if (output[i] != program[i])
                    {
                        same = false;
                        break;
                    }
                }
                if (same)
                    return toTry;
            }
            
            // Does program end with output?
            var ok = true;
            for (var i = 0; i < output.Count; i++)
            {
                if (program[^(i + 1)] != output[^(i + 1)])
                {
                    ok = false;
                    break;
                }
            }
            
            if (ok)
            {
                for (ulong i = 0; i < 8; i++)
                {
                    var next = i | (toTry << 3);
                    var value = Try(next);
                    if (value != 0)
                        return value;
                }
            }

            return 0;
        }
        
        void Run(ulong regA)
        {
            output.Clear();
            // Program: 2,4,1,7,7,5,1,7,4,6,0,3,5,5,3,0
            do
            {
                // 2,4
                var regB = regA % 8; // Take last 3 bits

                // 1,7
                var shiftBy = regB ^ 7; // and not them

                // 7,5
                var regC = regA >> (int)shiftBy; // lose between 0 and 7 bits

                // 4,6
                regB = regB ^ regC;

                // 5,5
                output.Add((int)(regB % 8));

                // 0,3
                regA = regA >> 3;
            } while (regA != 0);
        }
    }
}

