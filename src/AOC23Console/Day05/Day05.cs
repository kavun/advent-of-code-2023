using System.Text.RegularExpressions;

namespace AOC23Console.Day05;

public partial class Day05 : IDay
{
    public string Name => "Day 5: If You Give A Seed A Fertilizer";

    public record SeedRange(long Start, long Count)
    {
        public long End => Start + Count - 1;
    }

    public record SeedMaps()
    {
        public List<SeedMap> Maps = [];
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
    }

    public record SeedMapPartial(long DestinationStart, long SourceStart, long RangeLength)
    {

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

                    seedRanges.Add(new SeedRange(start, count));

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
        return finalRanges.MinBy(r => r.Start)!.Start;
    }
}
