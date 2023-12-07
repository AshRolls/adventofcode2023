using System.Runtime.ExceptionServices;

namespace AdventOfCode;

public class Day07 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day07()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    private void solve1()
    {
        List<Hand> hands = new List<Hand>();
        foreach (string s in _input)
        {
            Hand h = new Hand();
            for (int i = 0; i < 5; i++)
            {
                h.cards[i] = parseCard(s[i]);
            }
            h.bid = Int32.Parse(s[6..]);
            hands.Add(h);
        }

        Dictionary<int,List<Hand>> handTypes = new Dictionary<int, List<Hand>>();
        for(int i = 0; i<7; i++)
        {
            handTypes[i] = new List<Hand>();
        }

        foreach (Hand h in hands)
        {
            int singles = countDistinct(h);
            if (singles == 5) // high card
            {
                handTypes[6].Add(h);                
            }
            else if (singles == 4) // one pair
            {
                handTypes[5].Add(h);                
            }
            else if (singles == 1) // five of a kind
            {
                handTypes[0].Add(h);                
            }
            else if (singles == 2)
            {
                if(countMaxGroup(h) == 4) // four of a kind
                {
                    handTypes[1].Add(h);
                }
                else // full house
                {
                    handTypes[2].Add(h);
                }
            }
            else if (singles == 3)
            {
                if (countMaxGroup(h) == 3) // three of a kind
                {
                    handTypes[3].Add(h);
                }
                else // two pair
                {
                    handTypes[4].Add(h);
                }
            }
            else
            {
                throw new Exception();
            }                      
        }

        int rank = 1;
        int winnings = 0;
        for (int i = 6; i >= 0; i--)
        {
            handTypes[i].Sort();
            foreach(Hand h in handTypes[i])
            {
                winnings += h.bid * rank;
                rank++;
            }
        }

        _partOne = winnings.ToString();
    }

    private int countMaxGroup(Hand h)
    {
        return h.cards.GroupBy(x => x).Select(x => x.Count()).Max();
    }

    private int countDistinct(Hand h)
    {
        return h.cards.Distinct().Count();
    }

    private int parseCard(char v)
    {
        if (Char.IsDigit(v)) return int.Parse(v.ToString());
        else
        {
            switch (v)
            {
                case 'T':
                    return 10;
                case 'J':
                    return 11;
                case 'Q':
                    return 12;
                case 'K':
                    return 13;
                case 'A':
                    return 14;
            }
        }

        throw new Exception();
        return 0;
    }

    private class Hand : IComparable<Hand> 
    {
        public int[] cards = new int[5];
        public int bid { get; set; }

        public int CompareTo(Hand compareHand)
        {
            // A null value means that this object is greater.
            if (compareHand == null)
                return 1;

            else
            {
                for (int i=0; i<5; i++)
                {
                    if (this.cards[i] == compareHand.cards[i]) continue;
                    else return this.cards[i].CompareTo(compareHand.cards[i]);
                }
                return 0;
            }
        }
    }
    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        _partTwo = "Not Solved";
    }
}
