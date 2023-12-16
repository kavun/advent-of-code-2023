using AOC23Console.Day10;

namespace AOC23Tests;

public class Day10Tests
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(4, new Day10().Part1("""
            .....
            .S-7.
            .|.|.
            .L-J.
            .....
            """));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(0, new Day10().Part2("""

            """));
    }
}
