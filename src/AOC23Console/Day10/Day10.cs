namespace AOC23Console.Day10;

public partial class Day10 : IDay
{
    public string Number => "10";
    public string Name => $"Day {Number}: Pipe Maze";

    public delegate Loc[] Nav(Loc a);
    public Dictionary<char, Nav> Navs = new()
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
    };

    public record Node(Loc[] Connections, bool IsStart)
    {

    };

    public long Part1(string input)
    {
        var nodes = new Dictionary<Loc, Node>();
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var startX = -1;
        var startY = -1;
        var grid = lines.Select((l, y) => l.Select((c, x) =>
        {
            var loc = new Loc(x, y);
            var node = new Node(
                Connections: Navs.ContainsKey(c) ? Navs[c](loc) : [],
                IsStart: c == 'S');

            if (node.IsStart)
            {
                startX = x;
                startY = y;
            }

            Console.WriteLine($"{loc} {c} {string.Join(" ", node.Connections.AsEnumerable())}");

            return node;
        }).ToArray()).ToArray();

        var start = grid[startY][startX];

        var crossed = false;
        while (!crossed)
        {

        }


        return 0;
    }

    public long Part2(string input)
    {
        return 0;
    }
}
