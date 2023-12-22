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

    public record Node(char Char, Loc Self, Loc[] Connections, bool IsStart, bool IsPipe, bool IsEnclosed = false)
    {
        public char BoxChar => IsEnclosed ? '▓' : Char switch
        {
            'F' => '┌',
            '7' => '┐',
            'L' => '└',
            'J' => '┘',
            'S' => '⭐',
            '-' => '─',
            '|' => '│',
            _ => Char
        };
    }

    public long Part1(string input)
    {
        var (startX, startY, grid) = GetGrid(input);
        var seen = GetPath(startX, startY, grid);
        return (long)Math.Floor(seen.Count / 2m);
    }

    public (int startX, int startY, Node[][] grid) GetGrid(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var startX = -1;
        var startY = -1;
        var grid = lines
            .Select((l, y) => l
                .Select((c, x) =>
                {
                    var loc = new Loc(x, y);
                    var node = new Node(
                        Char: c,
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

        return (startX, startY, grid);
    }

    public HashSet<Loc> GetPath(int startX, int startY, Node[][] grid)
    {
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

        return seen;
    }

    public long Part2(string input)
    {
        var (startX, startY, grid) = GetGrid(input);
        var seen = GetPath(startX, startY, grid);

        Console.WriteLine($"Start: {startX} {startY}");
        Console.WriteLine();

        var enclosed = 0;
        var y = 0;
        // char? firstChar = default;
        foreach (var row in grid)
        {
            var x = 0;
            var enclosing = false;
            foreach (var node in row)
            {
                var partOfPipe = false;
                if (seen.Contains(node.Self))
                {
                    partOfPipe = true;

                    // https://github.com/fguchelaar/AdventOfCode2023/blob/efe911b568945729cb41e9ef93d57332a97a1c3c/day-10/Puzzle.cs#L65-L68
                    if (node.Char == '|' || node.Char == 'F' || node.Char == '7')
                    {
                        enclosing = !enclosing;
                    }

                    // https://www.reddit.com/r/adventofcode/comments/18evyu9/comment/keaz25j/?utm_source=share&utm_medium=web2x&context=3
                    // if (node.Char == '|')
                    // {
                    //     // we're crossing into or out of an enclosure
                    //     enclosing = !enclosing;
                    // }
                    // else if ("FL".Contains(node.Char))
                    // {
                    //     // the next char after this one will not be open
                    //     // but we keep track of this first one to determine
                    //     // if the following chars are opening or closing the enclosure
                    //     firstChar = node.Char;
                    // }
                    // else if (node.Char == 'J')
                    // {
                    //     if (firstChar == 'F')
                    //     {
                    //         // we're either ending or starting an enclosure
                    //         enclosing = !enclosing;
                    //     }
                    //     firstChar = null;
                    // }
                    // else if (node.Char == '7')
                    // {
                    //     if (firstChar == 'L')
                    //     {
                    //         // we're either ending or starting an enclosure
                    //         enclosing = !enclosing;
                    //     }
                    //     firstChar = null;
                    // }
                }
                else if (enclosing)
                {
                    enclosed++;
                    grid[y][x] = new Node(node.Char, node.Self, node.Connections, node.IsStart, node.IsPipe, IsEnclosed: true);
                }

                var c = grid[y][x];
                Console.Write(c.IsEnclosed || (c.IsPipe && partOfPipe) ? c.BoxChar : c.IsPipe ? '░' : ' ');
                x++;
            }
            Console.WriteLine();
            y++;
        }

        return enclosed;
    }
}
