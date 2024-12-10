using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace Day07;

public class Equation
{
    public long Target { get; set; }
    public List<long> Factors { get; set; }
    public List<List<string>> Operators { get; set; }
    public bool IsSolved { get; set; }
    public Equation(string line, int type)
    {
        var split = line.Split(':');
        Target = long.Parse(split[0]);
        Factors = split[1].Substring(1).Split(' ').Select(f => long.Parse(f)).ToList();
        var operatorsPositions = Factors.Count - 1;
        var operatorSet = type == 1 ? new List<string> { "add", "mul" } : new List<string> { "add", "mul" , "||"};
        Operators = Combination.GetCombinations(operatorsPositions, operatorSet);
        var allResults = Operators.Select(o => Calculator.CalculateLeftToRight(Factors, o)).ToList();
        IsSolved = allResults.Any(r =>r == Target);
    }

    private string GetNewLine(List<string> operators)
    {
        var factors = Factors.FirstOrDefault().ToString();
        for (int i = 0; i < operators.Count; i++)
        {
            var fac = Factors[i + 1].ToString();
            var ope = operators[i];
            factors += ope == "||" ? fac : (" " + fac);
        }
        var newLine = Target.ToString() + ": " + factors;
        return newLine;
    }

    public void PrintList<T>(List<List<T>> list)
    {
        foreach (var combination in list) Console.WriteLine(string.Join(',',combination));
    }
    
}