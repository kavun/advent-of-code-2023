using AOC23Console.Day09;

namespace AOC23Tests;

public class Day09Tests
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(114, new Day09().Part1("""
            0 3 6 9 12 15
            1 3 6 10 15 21
            10 13 16 21 30 45
            """));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(2, new Day09().Part2("""
            0 3 6 9 12 15
            1 3 6 10 15 21
            10 13 16 21 30 45
            """));
    }
}
