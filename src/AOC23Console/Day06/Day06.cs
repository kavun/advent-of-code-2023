using System.Runtime.InteropServices;

namespace AOC23Console.Day06;

public partial class Day06 : IDay
{
    public string Number => "06";
    public string Name => $"Day {Number}: Wait For It";

    public long Part1(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var times = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var distances = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

        var winnings = new List<int>();
        for (var i = 0; i < times.Length; i++)
        {
            var time = times[i];
            var distance = distances[i];
            var prev = -1L;
            var next = -1L;
            var timesWon = 0;
            for (var button = 0; button < time; button++)
            {
                next = Race(button, time);
                if (next > distance)
                {
                    timesWon++;
                }

                prev = next;
            }

            winnings.Add(timesWon);
        }

        return winnings.Aggregate((a, b) => a * b);
    }

    private static long Race(long button, long time)
    {
        var distance = (time - button) * button;
        return distance;
    }

    public long Part2(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var time = long.Parse(lines[0].Split(":")[1].Replace(" ", ""));
        var distance = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));

        var losingFromLeft = true;
        var start = 0;
        while (losingFromLeft)
        {
            if (Race(start, time) > distance)
            {
                losingFromLeft = false;
            }
            else
            {
                start++;
            }
        }

        var losingFromRight = true;
        var end = time;
        while (losingFromRight)
        {
            if (Race(end, time) > distance)
            {
                losingFromRight = false;
            }
            else
            {
                end--;
            }
        }

        return end - start + 1;
    }
}
