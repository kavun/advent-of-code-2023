using AOC23Console;
using AOC23Console.Day01;
using AOC23Console.Day02;
using AOC23Console.Day03;
using AOC23Console.Day04;
using AOC23Console.Day05;
using AOC23Console.Day06;
using System.Diagnostics;

Console.WriteLine();

var sw = new Stopwatch();

foreach (var day in new IDay[] {
    new Day01(),
    new Day02(),
    new Day03(),
    new Day04(),
    new Day05(),
    new Day06(),
}.Where(d => {
    if (args.Length == 0) return true;
    return args.Contains(d.Number);
}))
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
