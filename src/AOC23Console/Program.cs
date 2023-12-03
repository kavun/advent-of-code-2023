using AOC23Console;
using AOC23Console.Day01;
using AOC23Console.Day02;

foreach (var day in new IDay[] {
    new Day01(),
    new Day02(),
})
{
    Console.WriteLine($"""
        {day.Name}
          Part One: {day.Part1(File.ReadAllText($"{day.GetType().Name}/input.txt"))}
          Part Two: {day.Part2(File.ReadAllText($"{day.GetType().Name}/input.txt"))}
        """);
}