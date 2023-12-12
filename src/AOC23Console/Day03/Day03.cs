using System.Text;

namespace AOC23Console.Day03;

public partial class Day03 : IDay
{
    public string Name => "Day 3: Gear Ratios";

    public long Part1(string input)
    {
        var result = GetNumsAndGears(input);
        return result.Nums
            .Sum(n => n.IntNum);
    }

    public long Part2(string input)
    {
        var result = GetNumsAndGears(input);
        return result.Gears
            .Select(g =>
            {
                return new GearedNums(
                    Gear: g,
                    Nums: result.Nums.Where(n =>
                    {
                        return g.AdjTo(n);
                    }).ToList());
            })
            .Where(g => g.IsValid)
            .Sum(g => g.Ratio);
    }

    public record Num(string Number, int Start, int End, int Line)
    {
        public int IntNum => int.Parse(Number);
    }

    public record Gear(int Line, int Index)
    {
        public bool AdjTo(Num num)
        {
            return Line >= num.Line - 1 && Line <= num.Line + 1
                && Index >= num.Start - 1 && Index <= num.End + 1;
        }
    };

    public record NumsAndGears(List<Num> Nums, List<Gear> Gears);

    public record GearedNums(Gear Gear, List<Num> Nums)
    {
        public bool IsValid => Nums.Count == 2;
        public int Ratio => IsValid ? Nums[0].IntNum * Nums[1].IntNum : 0;
    }

    private static NumsAndGears GetNumsAndGears(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var nums = new List<Num>();
        var gears = new List<Gear>();

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var num = new StringBuilder();
            var building = false;
            var numStart = -1;

            void AddNumber(Num potentialNum)
            {
                var valid = false;

                for (var q = Math.Max(0, potentialNum.Start - 1); q < Math.Min(line.Length, potentialNum.End + 2); q++)
                {
                    if (i > 0)
                    {
                        if (lines[i - 1][q] != '.')
                        {
                            valid = true;
                        }
                    }

                    if (i < lines.Length - 1)
                    {
                        if (lines[i + 1][q] != '.')
                        {
                            valid = true;
                        }
                    }
                }

                if (potentialNum.Start > 0)
                {
                    if (lines[i][potentialNum.Start - 1] != '.')
                    {
                        valid = true;
                    }
                }

                if (potentialNum.End < line.Length - 1)
                {
                    if (lines[i][potentialNum.End + 1] != '.')
                    {
                        valid = true;
                    }
                }

                if (valid)
                {
                    nums.Add(potentialNum);
                }

                building = false;
                num.Clear();
                numStart = -1;
            }

            for (var l = 0; l < line.Length; l++)
            {
                var c = line[l];
                if (char.IsDigit(c))
                {
                    if (!building)
                    {
                        numStart = l;
                        building = true;
                    }
                    num.Append(c);
                    continue;
                }
                else if (building)
                {
                    var potentialNum = new Num(num.ToString(), numStart, l - 1, i);
                    AddNumber(potentialNum);
                }

                if (c == '*')
                {
                    gears.Add(new Gear(i, l));
                }
            }

            if (building)
            {
                var potentialNum = new Num(num.ToString(), numStart, line.Length, i);
                AddNumber(potentialNum);
            }
        }

        return new NumsAndGears(nums, gears);
    }
}
