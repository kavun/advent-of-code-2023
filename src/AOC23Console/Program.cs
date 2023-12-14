using AOC23Console;
using System.Diagnostics;

Console.WriteLine();

var sw = new Stopwatch();

var days = typeof(IDay).Assembly.GetTypes()
    .Where(t => t.GetInterfaces().Contains(typeof(IDay)))
    .Select(t => (IDay)Activator.CreateInstance(t)!)
    .OrderBy(d => d.Number)
    .Where(d =>
    {
        if (args.Length == 0) return true;
        return args.Contains(d.Number);
    });

foreach (var day in days)
{
    var input = File.ReadAllText($"{day.GetType().Name}/input.txt");

    Console.WriteLine(day.Name);

    var (part1, part1Time) = WithTimer(() => day.Part1(input));
    Console.WriteLine($"  Part One: {part1} ({part1Time}ms)");

    var (part2, part2Time) = WithTimer(() => day.Part2(input));
    Console.WriteLine($"  Part Two: {part2} ({part2Time}ms)");

    Console.WriteLine();
}

static (long, long) WithTimer(Func<long> action)
{
    var sw = new Stopwatch();
    sw.Start();
    var result = action();
    sw.Stop();
    return (result, sw.ElapsedMilliseconds);
}
