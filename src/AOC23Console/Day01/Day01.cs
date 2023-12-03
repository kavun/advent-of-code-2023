using System.Text.RegularExpressions;

namespace AOC23Console.Day01
{
    public partial class Day01
    {
        public static int Part1_SumOfCalibration(string input)
        {
            return input
                .Split(Environment.NewLine)
                .Sum(line =>
                {
                    var values = NumbersRegex()
                        .Matches(line)
                        .Select(m => m.Value);
                    var strValue = $"{values.First()}{values.Last()}";
                    var value = int.Parse(strValue);
                    return value;
                });
        }

        [GeneratedRegex(@"[0-9]")]
        private static partial Regex NumbersRegex();
    }
}
