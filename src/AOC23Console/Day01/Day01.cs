using System.Text.RegularExpressions;

namespace AOC23Console.Day01
{
    public partial class Day01 : IDay
    {
        public string Name => "Day 1: Trebuchet?!";

        public int Part1(string input)
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

        [GeneratedRegex(@"[1-9]")]
        private static partial Regex NumbersRegex();

        public int Part2(string input)
        {
            return input
                .Split(Environment.NewLine)
                .Sum(line =>
                {
                    var values = NumbersAndWordsRegex()
                        .Matches(line)
                        .Select(m =>
                        {
                            var val = m.Groups[1].Value;
                            var updated = val switch
                            {
                                "one" => "1",
                                "two" => "2",
                                "three" => "3",
                                "four" => "4",
                                "five" => "5",
                                "six" => "6",
                                "seven" => "7",
                                "eight" => "8",
                                "nine" => "9",
                                _ => val
                            };

                            return updated;
                        })
                        .ToList();

                    return int.Parse($"{values.First()}{values.Last()}");
                });
        }

        // https://stackoverflow.com/a/320478/789893
        [GeneratedRegex(@"(?=([1-9]|one|two|three|four|five|six|seven|eight|nine))")]
        private static partial Regex NumbersAndWordsRegex();
    }
}
