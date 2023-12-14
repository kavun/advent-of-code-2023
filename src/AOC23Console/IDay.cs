namespace AOC23Console;

public interface IDay
{
    long Part1(string input);
    long Part2(string input);
    string Name { get; }
    string Number { get; }
}
