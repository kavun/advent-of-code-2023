using System.Text.RegularExpressions;

namespace AOC23Console.Day05;

public partial class Day05 : IDay
{
    public string Number => "05";
    public string Name => $"Day {Number}: If You Give A Seed A Fertilizer";

    public record SeedRange
    {
        public SeedRange(long Start, long End)
        {
            this.Start = Start;
            this.End = End;
        }
        public long Start {get;set;}
        public long End {get;set;}
        public override string ToString() => $"{Start}..{End}";
    }

    public record SeedMaps()
    {
        public List<SeedMap> Maps = [];
        public override string ToString() => string.Join(Environment.NewLine, Maps.Select(m => string.Join(Environment.NewLine, m.Partials.Select(p => $"{p.SourceStart}..{p.SourceEnd} -> {p.DestinationStart}..{p.DestinationEnd}"))));
        public void Add(SeedMap map)
        {
            Maps.Add(map);
        }

        public long Find(long seed)
        {
            var m = -1;
            foreach (var map in Maps)
            {
                m++;
                var mapped = map.Find(seed);
                if (mapped != seed)
                {
                    //Console.WriteLine($"{seed} mapped to {mapped} by map {m}");
                    seed = mapped;
                }
                else
                {
                    //Console.WriteLine($"{seed} not mapped by map {m}");
                }
            }

            return seed;
        }

        public List<SeedRange> WalkRanges(List<SeedRange> seedRanges)
        {
            foreach (var map in Maps)
            {
                var mappedRanges = new List<SeedRange>();
                foreach (var range in seedRanges)
                {
                    // Console.WriteLine("mapping " + range);

                    foreach (var partial in map.Partials.OrderBy(p => p.SourceStart))
                    {
                        var offset = partial.DestinationStart - partial.SourceStart;
                        // Console.WriteLine("using partial " + partial + " with offset " + offset);

                        if (range.Start < partial.SourceStart)
                        {
                            /*
                                range: 2..6
                                partial: 5..7 -> 10..12

                                mappedRanges: 2..4
                                range becomes: 5..6
                            */
                            var newRange = new SeedRange(
                                Start: range.Start,
                                End: Math.Min(range.End, partial.SourceStart - 1)
                            );
                            mappedRanges.Add(newRange);
                            range.Start = partial.SourceStart;
                            // Console.WriteLine("1. added " + newRange + " and updated range to " + range);
                            // Console.WriteLine(range);
                            if (range.Start > range.End)
                            {
                                break;
                            }
                        }

                        if (range.Start <= partial.SourceEnd)
                        {
                            /*
                                range: 5..6
                                partial: 5..7 -> 10..12

                                mappedRanges: 10..11
                                range becomes: 8..6 // start > end, so we're done with map.Partials
                            */
                            var newRange = new SeedRange(
                                Start: range.Start + offset,
                                End: Math.Min(partial.DestinationEnd, range.End + offset)
                            );
                            mappedRanges.Add(newRange);
                            range.Start = partial.SourceEnd + 1;
                            // Console.WriteLine("2. added " + newRange + " and updated range to " + range);
                            if (range.Start > range.End)
                            {
                                break;
                            }
                        }
                    }

                    // if we didn't map the whole range, add the remainder
                    if (range.Start <= range.End)
                    {
                        mappedRanges.Add(range);
                        // Console.WriteLine("3. added remaining " + range);
                    }
                }

                seedRanges = mappedRanges;
            }

            return seedRanges;
        }
    }

    public record SeedMap(List<SeedMapPartial> Partials)
    {
        public long Find(long seed)
        {
            foreach (var partial in Partials)
            {
                if (seed >= partial.SourceStart && seed <= partial.SourceStart + partial.RangeLength)
                {
                    return partial.DestinationStart + (seed - partial.SourceStart);
                }
            }

            return seed;
        }

        public override string ToString() => string.Join(Environment.NewLine, Partials.Select(p => $"{p.SourceStart}..{p.SourceEnd} -> {p.DestinationStart}..{p.DestinationEnd}"));
    }

    public record SeedMapPartial(long DestinationStart, long SourceStart, long RangeLength)
    {
        public long SourceEnd => SourceStart + RangeLength - 1;
        public long DestinationEnd => DestinationStart + RangeLength - 1;
        public override string ToString() => $"{SourceStart}..{SourceEnd} -> {DestinationStart}..{DestinationEnd}";
    }

    public long Part1(string input)
    {
        var matches = Regex.Matches(input, @":([^a-z]+)");
        var seeds = new HashSet<long>();
        var seedMaps = new SeedMaps();

        var blockIdx = 0;
        foreach (var match in matches.ToArray())
        {
            var block = match.Groups[1].Value.Trim();

            if (blockIdx == 0)
            {
                seeds = new HashSet<long>(block.Split(" ").Select(b => long.Parse(b)));
            }
            else
            {
                seedMaps.Add(GetSeedMap(block));
            }

            blockIdx++;
        }

        var seedLocations = new List<long>();
        foreach (var seed in seeds)
        {
            var location = seedMaps.Find(seed);
            seedLocations.Add(location);
        }

        return seedLocations.Min();
    }

    private static SeedMap GetSeedMap(string block)
    {
        var partials = new List<SeedMapPartial>();
        foreach (var blockLine in block.Split(Environment.NewLine))
        {
            var blockLineParts = blockLine.Split(" ");
            var partial = new SeedMapPartial(
                DestinationStart: long.Parse(blockLineParts[0]),
                SourceStart: long.Parse(blockLineParts[1]),
                RangeLength: long.Parse(blockLineParts[2])
            );
            partials.Add(partial);
        }
        return new SeedMap(partials);
    }


    public long Part2(string input)
    {
        var matches = Regex.Matches(input, @":([^a-z]+)");
        var seedRanges = new List<SeedRange>();
        var seedMaps = new SeedMaps();

        var blockIdx = 0;
        foreach (var match in matches.ToArray())
        {
            var block = match.Groups[1].Value.Trim();

            if (blockIdx == 0)
            {
                var seedParts = block.Split(" ");
                var idx = seedParts.Length - 1;
                // Console.WriteLine(idx);

                while (idx > 0)
                {
                    var start = long.Parse(seedParts[idx - 1]);
                    var count = long.Parse(seedParts[idx]);

                    // while (count > 0) {
                    //     count--;
                    //     seeds.Add(start + count);
                    // }

                    // can't brute force billions of seeds, build ranges instead

                    seedRanges.Add(new SeedRange(
                        Start: start,
                        End: start + count - 1));

                    idx -= 2;
                }
            }
            else
            {
                seedMaps.Add(GetSeedMap(block));
            }

            blockIdx++;
        }

        List<SeedRange> finalRanges = seedMaps.WalkRanges(seedRanges);
        return finalRanges.Min(r => r.Start);
    }
}
