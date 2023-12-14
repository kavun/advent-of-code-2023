using AOC23Console.Day06;

namespace AOC23Tests;

public class Day06Tests
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(288, new Day06().Part1("""
            Time:      7  15   30
            Distance:  9  40  200
            """));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(71503, new Day06().Part2("""
            Time:      7  15   30
            Distance:  9  40  200
            """));
    }
}
