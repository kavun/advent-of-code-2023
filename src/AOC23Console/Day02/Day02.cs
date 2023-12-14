using System.Text.RegularExpressions;

namespace AOC23Console.Day02;

public partial class Day02 : IDay
{
    public string Number => "02";
    public string Name => $"Day {Number}: Cube Conundrum";

    public record Game(int Id, int Red, int Green, int Blue)
    {
        public bool IsPossible => Red <= 12 && Green <= 13 && Blue <= 14;
        public int Power => Red * Green * Blue;
    }

    public long Part1(string input)
    {
        return input.Split(Environment.NewLine)
            .Select(GetGame)
            .Where(g => g.IsPossible)
            .Sum(g => g.Id);
    }

    private static Game GetGame(string line)
    {
        var id = int.Parse(GameIdRegex().Match(line).Groups[1].Value);
        var red = RedCubesRegex().Matches(line).Max(m => int.Parse(m.Groups[1].Value));
        var green = GreenCubesRegex().Matches(line).Max(m => int.Parse(m.Groups[1].Value));
        var blue = BlueCubesRegex().Matches(line).Max(m => int.Parse(m.Groups[1].Value));
        var g = new Game(Id: id, Red: red, Green: green, Blue: blue);
        return g;
    }

    public long Part2(string input)
    {
        return input.Split(Environment.NewLine)
            .Select(GetGame)
            .Sum(g => g.Power);
    }

    [GeneratedRegex(@"^Game ([0-9]+):")]
    private static partial Regex GameIdRegex();
    [GeneratedRegex(@" ([0-9]+) red")]
    private static partial Regex RedCubesRegex();
    [GeneratedRegex(@" ([0-9]+) green")]
    private static partial Regex GreenCubesRegex();
    [GeneratedRegex(@" ([0-9]+) blue")]
    private static partial Regex BlueCubesRegex();
}
