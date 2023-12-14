using AOC23Console.Day07;

namespace AOC23Tests;

public class Day07Tests
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(6440, new Day07().Part1("""
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(5905, new Day07().Part2("""
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """));
    }
}
