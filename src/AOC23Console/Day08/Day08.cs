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

    public record Node(string[] Paths, bool IsZ, bool IsA);

    public long Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var dirs = lines[0].Trim().ToCharArray().Select(c => c == 'L' ? 0 : 1).ToArray();

        var pathsDict = lines[1..].Select(l =>
        {
            var parts = l.Split(" = ");
            var source = parts[0];
            var lr = parts[1].Split(", ").Select(c => c.Trim().Trim('(').Trim(')')).ToArray();
            return (source, new string[] { lr[0], lr[1] }, source.EndsWith('Z'), source.EndsWith('A'));
        }).ToDictionary(kvp => kvp.source, kvp => new KeyValuePair<string, Node>(kvp.source, new Node(kvp.Item2, kvp.Item3, kvp.Item4)));

        var locations = pathsDict.Where(k => k.Value.Value.IsA).ToArray();
        var steps = locations
            .Select(l =>
            {
                var steps = 0L;
                var _l = l.Value;
                while (!_l.Value.IsZ)
                {
                    var dir = dirs[steps % dirs.Length];
                    _l = pathsDict[_l.Value.Paths[dir]];
                    steps++;
                }

                return steps;
            })
            // paths loop from back immediately from Z to A, so we need to find the least common multiple of all paths
            .Aggregate(GetLeastCommonMultiple);

        return steps;
    }

    public static long GetLeastCommonMultiple(long a, long b)
    {
        return a / GetGreatestCommonDivisor(a, b) * b;
    }

    public static long GetGreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

}
