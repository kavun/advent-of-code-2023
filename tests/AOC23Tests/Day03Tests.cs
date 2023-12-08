using AOC23Console.Day03;

namespace AOC23Tests
{
    public class Day03Tests
    {
        [Fact]
        public void Part1()
        {
            Assert.Equal(4361, new Day03().Part1("""
                467..114..
                ...*......
                ..35..633.
                ......#...
                617*......
                .....+.58.
                ..592.....
                ......755.
                ...$.*....
                .664.598..
                """));
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(467835, new Day03().Part2("""
                467..114..
                ...*......
                ..35..633.
                ......#...
                617*......
                .....+.58.
                ..592.....
                ......755.
                ...$.*....
                .664.598..
                """));
        }
    }
}
