using System.Xml.Serialization;

namespace Day12;

public class Rule
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Line { get; set; }
    public int MaxPress { get; set; }
    public Rule(string line, string key)
    {
        Line = line;
        var rule = line.Split(": ")[1].Split(", ");
        X = Game.GetValueStartWith(rule, "X+");
        Y = Game.GetValueStartWith(rule, "Y+");
    }

    public void GetMaxPush(Game game)
    {
        var maxX = game.Target[0] / X + 1;
        var maxY = game.Target[1] / Y + 1;
        MaxPress = (int) Math.Min(maxX, maxY);
    }
}