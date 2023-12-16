namespace AOC23Console.Day09;

public partial class Day09 : IDay
{
    public string Number => "09";
    public string Name => $"Day {Number}: Mirage Maintenance";

    public long Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return lines.Sum(line =>
        {
            var nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            var subs = new List<List<long>>() { nums };
            var allZero = false;
            var nextSub = nums;
            while (!allZero)
            {
                allZero = true;
                var sub = new List<long>();
                for (var i = 0; i < nextSub.Count - 1; i++)
                {
                    var a = nextSub[i];
                    var b = nextSub[i + 1];
                    var result = b - a;
                    if (result != 0)
                    {
                        allZero = false;
                    }
                    sub.Add(result);
                }

                subs.Add(sub);
                nextSub = sub;
            }

            long GetNextValue(int currIdx)
            {
                var next = 0L;
                if (currIdx >= 0 && currIdx < subs.Count - 1)
                {
                    next = GetNextValue(currIdx + 1);
                }
                var last = subs[currIdx].Last();
                // Console.WriteLine($"{string.Join(" ", subs[currIdx])} + {last + next}");
                return last + next;
            }

            var next = GetNextValue(0);
            // Console.WriteLine();
            return next;
        });
    }

    public long Part2(string input)
    {
        return 0;
    }
}
