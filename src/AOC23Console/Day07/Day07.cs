namespace AOC23Console.Day07;

public partial class Day07 : IDay
{
    public string Number => "07";
    public string Name => $"Day {Number}: Camel Cards";

    private static readonly Dictionary<char, int> _cardRanks = new()
    {
        {'2', 2},
        {'3', 3},
        {'4', 4},
        {'5', 5},
        {'6', 6},
        {'7', 7},
        {'8', 8},
        {'9', 9},
        {'T', 10},
        {'J', 11},
        {'Q', 12},
        {'K', 13},
        {'A', 14},
    };
    private static readonly Dictionary<int, char> _rankCards = _cardRanks.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

    public long Part1(string input)
    {
        var hands = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(line => new Hand(line[0], line[1]));

        var finalOrder = new List<Hand>();
        foreach (var rankGroup in hands.GroupBy(h => h.Rank).OrderByDescending(g => g.Key))
        {
            var rerankedGroup = rankGroup
                .OrderByDescending(h => h.Cards[0])
                .ThenByDescending(h => h.Cards[1])
                .ThenByDescending(h => h.Cards[2])
                .ThenByDescending(h => h.Cards[3])
                .ThenByDescending(h => h.Cards[4]);
            finalOrder.AddRange(rerankedGroup);
        }

        var total = 0;
        var hIdx = finalOrder.Count;
        foreach (var hand in finalOrder)
        {
            total += hand.Bid * hIdx;
            hIdx--;
        }

        return total;
    }

    public class Hand
    {
        public Hand(string hand, string bid)
        {
            HandStr = hand;
            Bid = int.Parse(bid);
            Cards = HandStr.ToCharArray().Select(c => _cardRanks[c]).ToArray();
            CardCounts = Cards
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());
            var counts = CardCounts.Values.ToArray();
            Rank = CardCounts.First().Value switch
            {
                5 => 5,
                4 => 4,
                3 => 3 + (counts[1] == 2 ? .1m : 0),
                2 => 2 + (counts[1] == 2 ? .1m : 0),
                _ => -1
            };
        }

        public int[] Cards { get; }
        public Dictionary<int, int> CardCounts { get; }

        public string HandStr { get; }
        public int Bid { get; }
        public override string ToString()
        {
            return $"""
                {HandStr} {Bid}
                {string.Join(" ", Cards)}
                  {string.Join("\n  ", CardCounts.Select(kvp => $"{_rankCards[kvp.Key]}:{kvp.Value}"))}
                """;
        }

        public decimal Rank { get; }
    }

    public long Part2(string input)
    {
        return 0;
    }
}
