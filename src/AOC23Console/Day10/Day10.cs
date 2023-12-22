namespace AOC23Console.Day10;

public partial class Day10 : IDay
{
    public string Number => "10";
    public string Name => $"Day {Number}: Pipe Maze";

    public delegate Loc[] Nav(Loc a);
    public Dictionary<char, Nav> CharNav = new()
    {
        { '|', a => new Loc[] { new(a.X, a.Y - 1), new(a.X, a.Y + 1) } },
        { '-', a => new Loc[] { new(a.X - 1, a.Y), new(a.X + 1, a.Y) } },
        { 'F', a => new Loc[] { new(a.X, a.Y + 1), new(a.X + 1, a.Y) } },
        { '7', a => new Loc[] { new(a.X - 1, a.Y), new(a.X, a.Y + 1) } },
        { 'L', a => new Loc[] { new(a.X + 1, a.Y), new(a.X, a.Y - 1) } },
        { 'J', a => new Loc[] { new(a.X - 1, a.Y), new(a.X, a.Y - 1) } },
        { 'S', a => new Loc[] { new(a.X, a.Y - 1), new(a.X, a.Y + 1), new(a.X - 1, a.Y), new(a.X + 1, a.Y) } },
    };

    public record Loc(int X, int Y)
    {
        public bool CanNavWith(Node[][] grid) => Y >= 0 && Y < grid.Length && X >= 0 && X < grid[Y].Length;
        public Node NavWith(Node[][] grid) => grid[Y][X];
        public bool IsPipe(Node[][] grid) => grid[Y][X].IsPipe;
        public override string ToString() => $"({X}, {Y})";
    };

    public record Node(Loc Self, Loc[] Connections, bool IsStart, bool IsPipe);

    public long Part1(string input)
    {
        var nodes = new Dictionary<Loc, Node>();
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var startX = -1;
        var startY = -1;
        var grid = lines
            .Select((l, y) => l
                .Select((c, x) =>
                {
                    var loc = new Loc(x, y);
                    var node = new Node(
                        Self: loc,
                        Connections: CharNav.ContainsKey(c) ? CharNav[c](loc) : [],
                        IsStart: c == 'S',
                        IsPipe: CharNav.ContainsKey(c));

                    if (node.IsStart)
                    {
                        startX = x;
                        startY = y;
                    }

                    // Console.WriteLine($"{loc} {c} {string.Join(" ", node.Connections.AsEnumerable())}");

                    return node;
                })
                .ToArray()
            )
            .ToArray();

        // Console.WriteLine();
        // Console.WriteLine($"Start: {startX} {startY}");
        // Console.WriteLine();

        var start = grid[startY][startX];
        var pending = new List<Node> { start };
        var seen = new HashSet<Loc>(pending.Select(n => n.Self));
        var crossed = false;
        while (!crossed)
        {
            var pendingIds = new HashSet<Loc>(pending.Select(n => n.Self));
            var next = pending.SelectMany(n => n.Connections)
                .Where(c => c.CanNavWith(grid))
                .Select(c => c.NavWith(grid))
                .Where(n => !pendingIds.Contains(n.Self))
                .Where(n => n.IsPipe)
                .Where(n => !seen.Contains(n.Self))
                .ToList();

            // Console.WriteLine($"Current: {string.Join(" ", pending.Select(n => n.Self))}");
            // Console.WriteLine($"Next: {string.Join(" ", next.Select(n => n.Self))}");

            if (next.All(n => seen.Contains(n.Self)))
            {
                // Console.WriteLine($"Crossed: {seen.Count}");
                crossed = true;
            }
            else
            {
                foreach (var n in next)
                {
                    seen.Add(n.Self);
                }
                pending = next;
            }
        }

        return (long)Math.Floor(seen.Count / 2m);
    }

    public long Part2(string input)
    {
        return 0;
    }
}
