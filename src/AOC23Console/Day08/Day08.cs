namespace AOC23Console.Day08;

public partial class Day08 : IDay
{
    public string Number => "08";
    public string Name => $"Day {Number}: Haunted Wasteland";

    public long Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var dirs = lines[0].Trim().ToCharArray().Select(c => c == 'L' ? 0 : 1).ToArray();

        var paths = lines[1..].Select(l =>
        {
            var parts = l.Split(" = ");
            var source = parts[0];
            var lr = parts[1].Split(", ").Select(c => c.Trim().Trim('(').Trim(')')).ToArray();
            return (source, new string[] { lr[0], lr[1] });
        }).ToDictionary(kvp => kvp.source, kvp => kvp.Item2);

        var location = "AAA";
        var steps = 0;
        while (location != "ZZZ")
        {
            var dir = dirs[steps % dirs.Length];
            location = paths[location][dir];
            steps++;
        }

        return steps;
    }

    public long Part2(string input)
    {
        return 0;
    }
}
