using AOC23Console.Day01;

namespace AOC23Tests
{
    public class Day01Tests
    {
        [Fact]
        public void SumOfCalibration()
        {
            Assert.Equal(153, Day01.Part1_SumOfCalibration("""
                1abc2
                pqr3stu8vwx
                a1b2c3d4e5f
                treb7uchet
                asdf13421asf
                """));
        }
    }
}
