using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17;

public class Part2
{
    private ulong _best = ulong.MaxValue;
    private string _expectedOutput = "2415751643550330";
    public Part2 ()
    {
    }

    public string Revers()
    {
        Solve(0UL, _expectedOutput.Length - 1);
        return _best.ToString();
    }

    private void Solve(ulong currentA, int index)
    {
        if (index == -1)
        {
            _best = Math.Min(_best, currentA);
            return;
        }

        var next = _expectedOutput[index];
        for (int remainder = 0; remainder < 8; remainder++)
        {
            var nextA = currentA * 8 + (ulong)remainder;
            var result = Execute(nextA);
            if (_expectedOutput.EndsWith(result))
            {
                Solve(nextA, index - 1);
            }
        }
    }

    private string Execute(ulong a)
    {
        StringBuilder output = new();
        do
        {
            ulong b = a % 8;
            b = b ^ 5;
            ulong c = a / (ulong)Math.Pow(2, b);
            b = b ^ 6;
            b = b ^ c;
            output.Append((b % 8).ToString());
            a = a / 8;
        } while (a != 0);

        return output.ToString();
    }
}
