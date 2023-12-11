namespace AOC23Console.Day04;

public partial class Day04 : IDay
{
    public string Name => "Day 4: Scratchcards";

    public int Part1(string input)
    {
        return input.Split(Environment.NewLine)
            .Sum(line =>
            {
                var parts = line.Split(":")[1].Split("|");
                var winning = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var have = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var winningNums = new HashSet<int>(winning.Select(w => int.Parse(w)));
                var haveNums = new HashSet<int>(have.Select(h => int.Parse(h)));
                var winningCount = winningNums.Where(w => haveNums.Contains(w)).Count();
                var score = (int)Math.Pow(2, winningCount - 1);
                return score;
            });
    }

    public int Part2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var extras = new Dictionary<int, int>();
        for (var lineNum = 0; lineNum < lines.Length; lineNum++) {
            extras.Add(lineNum, 1);
        }

        var l = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(":")[1].Split("|");
            var winning = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var have = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var winningNums = new HashSet<int>(winning.Select(w => int.Parse(w)));
            var haveNums = new HashSet<int>(have.Select(h => int.Parse(h)));
            var winningCount = winningNums.Where(w => haveNums.Contains(w)).Count();
            var nextIdx = l + 1;
            var max = Math.Min(lines.Length, nextIdx + winningCount);
            for (var e = nextIdx; e < max; e++) {
                extras[e] = extras[e] + extras[l];
            }

            l++;
        }

        return extras.Values.Sum();
    }
}
