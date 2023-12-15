using AOC23Console.Day08;

namespace AOC23Tests;

public class Day08Tests
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(2, new Day08().Part1("""
            RL

            AAA = (BBB, CCC)
            BBB = (DDD, EEE)
            CCC = (ZZZ, GGG)
            DDD = (DDD, DDD)
            EEE = (EEE, EEE)
            GGG = (GGG, GGG)
            ZZZ = (ZZZ, ZZZ)
            """));

        Assert.Equal(6, new Day08().Part1("""
            LLR

            AAA = (BBB, BBB)
            BBB = (AAA, ZZZ)
            ZZZ = (ZZZ, ZZZ)
            """));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(0, new Day08().Part2("""

            """));
    }
}
