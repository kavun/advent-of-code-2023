using AOC23Console.Day01;

namespace AOC23Tests
{
    public class Day01Tests
    {
        [Fact]
        public void Part1_SumOfCalibration()
        {
            Assert.Equal(153, new Day01().Part1("""
                1abc2
                pqr3stu8vwx
                a1b2c3d4e5f
                treb7uchet
                asdf13421asf
                """));
        }

        [Fact]
        public void Part2_SumOfCalibration()
        {
            Assert.Equal(363, new Day01().Part2("""
                two1nine
                eightwothree
                abcone2threexyz
                xtwone3four
                4nineeightseven2
                zoneight234
                7pqrstsixteen
                eighteightsrfcxtvg7three1two9nineeightwolqn
                """));
        }
    }
}
