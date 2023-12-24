namespace AOC23Console.Day11;

public partial class Day11 : IDay
{
    public string Number => "11";
    public string Name => $"Day {Number}: Cosmic Expansion";

    private record Space(char Char, int X, int Y)
    {
        public bool IsEmpty => Char == '.';
        public bool IsGalaxy => Char == '#';
        public override string ToString() => $"{Char} ({X}, {Y})";
    }

    private record SpacePair(Space A, Space B);

    public long Part1(string input)
    {
        var cosmos = BuildCosmos(input);

        // PrintCosmos(cosmos);

        cosmos = ExpandCosmos(cosmos);

        var pairs = GetGalaxyPairs(cosmos);

        var pathLengths = pairs.Select(CalculateLengthBetweenPair).ToList();

        // PrintCosmos(cosmos);

        return pathLengths.Sum();
    }

    private static int CalculateLengthBetweenPair(SpacePair p, int i)
    {
        var length = Math.Abs(p.B.Y - p.A.Y) + Math.Abs(p.B.X - p.A.X);
        // Console.WriteLine($"{i}: {p.A} -> {p.B} = {length}");
        return length;
    }

    private static HashSet<SpacePair> GetGalaxyPairs(List<List<Space>> cosmos)
    {
        return new HashSet<SpacePair>(cosmos
            .SelectMany(y => y)
            .Where(s => s.IsGalaxy)
            .SelectMany(a => cosmos
                .SelectMany(y => y)
                .Where(s => s.IsGalaxy && s != a)
                .Select(b => a.X < b.X || (a.X == b.X && a.Y < b.Y)
                    ? new SpacePair(a, b)
                    : new SpacePair(b, a)))
            .ToList()
        );
    }

    private static List<List<Space>> BuildCosmos(string input)
    {
        return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select((line, y) => line.ToCharArray().Select((@char, x) => new Space(@char, x, y)).ToList())
            .ToList();
    }

    private static void PrintCosmos(List<List<Space>> cosmos)
    {
        Console.WriteLine();
        foreach (var y in cosmos)
        {
            Console.WriteLine(string.Join("", y.Select(s => s.Char)));
        }
    }

    private static List<List<Space>> ExpandCosmos(List<List<Space>> cosmos)
    {
        // 1. expand vertical
        var yCount = 0;
        foreach (var y in cosmos.Where(s => s.All(c => c.IsEmpty)).Select(l => l.First().Y).ToArray())
        {
            cosmos.Insert(y + yCount, cosmos[y].Select(s => new Space('.', s.X, s.Y)).ToList());
            yCount++;
        }

        cosmos = RebuildCoordinates(cosmos);

        // 2. expand horizontal
        var xCount = 0;
        foreach (var x in cosmos.First().Where((s, x) => cosmos.All(y => y[x].IsEmpty)).Select(s => s.X).ToArray())
        {
            foreach (var y in cosmos)
            {
                y.Insert(x + xCount, new Space('.', x, y.First().Y));
            }
            xCount++;
        }

        cosmos = RebuildCoordinates(cosmos);

        return cosmos;
    }

    private static List<List<Space>> RebuildCoordinates(List<List<Space>> cosmos)
    {
        cosmos = cosmos.Select((y, yi) => y.Select((x, xi) => new Space(x.Char, xi, yi)).ToList()).ToList();
        return cosmos;
    }

    public long Part2(string input)
    {
        return 0;
    }
}
