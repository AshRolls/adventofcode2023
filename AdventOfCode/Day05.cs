namespace AdventOfCode;

public class Day05 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day05()
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
        var seeds = AoCHelper.GetLongNumsFromStr(_input[0]);
        int i = 0;
        Dictionary<int, List<Map>> listMaps = new Dictionary<int, List<Map>>
        {
            { 0, new List<Map>() }
        };
        foreach (string s in _input.Skip(3))
        {
            if (s.Contains(':'))
            {
                i++;
                listMaps.Add(i, new List<Map>());
                continue;
            }

            var cur = AoCHelper.GetLongNumsFromStr(s);
            if (cur.Length > 0)
            {
                Map m = new Map(cur[0], cur[1], cur[2]);
                listMaps[i].Add(m);
            }            
        }

        List<long> locs = new List<long>();
        foreach (long seed in seeds)
        {
            long cur = seed;
            foreach (List<Map> listMap in listMaps.Values)
            {
                foreach(Map m in listMap)
                {
                    if (cur >= m.sourceMin && cur <= m.sourceMax)                        
                    {
                        cur = m.destMin + (cur - m.sourceMin);
                        break;
                    }
                }
            }
            locs.Add(cur);
        }

        _partOne = locs.Min().ToString();
    }

    private class Map
    {
        public long destMin { get; set; }
        public long destMax { get; set; }
        public long sourceMin { get; set; }
        public long sourceMax { get; set; }
        public long range { get; set; }

        public Map(long dest, long source, long r) 
        { 
            destMin = dest;
            destMax = dest + r;
            sourceMin = source;
            sourceMax = source + r;
            range = r;
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
