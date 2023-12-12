using System.Text.RegularExpressions;

namespace AOC23Console.Day05;

public partial class Day05 : IDay
{
    public string Name => "Day 5: If You Give A Seed A Fertilizer";

    public record SeedMaps()
    {
        public List<SeedMap> Maps = [];
        public void Add(SeedMap map)
        {
            Maps.Add(map);
        }

        public long Find(long seed)
        {
            //Console.WriteLine();
            //Console.WriteLine($"Finding {seed}");

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

            //Console.WriteLine();
            //Console.WriteLine(block);
            //Console.WriteLine();

            if (blockIdx == 0)
            {
                seeds = new HashSet<long>(block.Split(" ").Select(b => long.Parse(b)));
            }
            else
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
                    //Console.WriteLine(partial);
                    partials.Add(partial);
                }
                seedMaps.Add(new SeedMap(partials));
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

    public long Part2(string input)
    {
        return 0;
    }
}
