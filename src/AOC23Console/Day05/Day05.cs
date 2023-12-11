using System.Text.RegularExpressions;

namespace AOC23Console.Day05;

public partial class Day05 : IDay
{
    public string Name => "Day 5: If You Give A Seed A Fertilizer";

    public int Part1(string input)
    {
        var matches = Regex.Matches(input, @":([^a-z]+)");
        var seeds = new HashSet<int>();
        var blockIdx = 0;
        foreach (var match in matches.ToArray()) {
            var block = match.Groups[1].Value.Trim();

            switch (blockIdx) {
                case 0:
                    seeds = new HashSet<int>(block.Split(" ").Select(b => int.Parse(b)));
                    break;
            }

            blockIdx++;
        }

        Console.WriteLine(string.Join(", ", seeds));

        return 0;
    }

    public int Part2(string input)
    {
        return 0;
    }
}
