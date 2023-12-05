
using System.Collections.Concurrent;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        long[] seeds = AoCHelper.GetLongNumsFromStr(_input[0]);
        Dictionary<int, List<Map>> listMaps;
        parseMaps(out listMaps);

        long lowestLoc = long.MaxValue;
        foreach (long seed in seeds)
        {
            long curLoc = getLocations(seed, 1, listMaps);
            if (curLoc < lowestLoc) lowestLoc = curLoc;
        }
        
        _partOne = lowestLoc.ToString();
    }

    static readonly object lockObject = new object();
    private static long getLocations(long seed, long range, Dictionary<int, List<Map>> listMaps)
    {
        Console.Out.WriteLine("Checking seed {0} : range {1}", seed, range);
        long lowestLoc = long.MaxValue;       
        // brute force it! ugly
        Parallel.For(0, range, i =>             
        {
            long cur = seed + i;
            foreach (List<Map> listMap in listMaps.Values)
            {
                foreach (Map m in listMap)
                {
                    if (cur >= m.sourceMin && cur < m.sourceMax)
                    {
                        cur = m.destMin + (cur - m.sourceMin);
                        break;
                    }
                }                           
            }
            lock (lockObject)
            {
                if (cur < lowestLoc) lowestLoc = cur;
            }
        }
        );

        return lowestLoc;
    }

    private void parseMaps(out Dictionary<int, List<Map>> listMaps)
    {       
        int i = 0;
        listMaps = new Dictionary<int, List<Map>>
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

        foreach(int m in listMaps.Keys)
        {
            listMaps[m].Sort((x, y) => x.sourceMin.CompareTo(y.sourceMin));
        }
    }

    private struct Map
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
        long[] seeds = AoCHelper.GetLongNumsFromStr(_input[0]);
        Dictionary<int, List<Map>> listMaps;
        parseMaps(out listMaps);

        long lowestLoc = long.MaxValue;
        for (int i = 0; i < seeds.Length; i += 2)
        {           
            long curLoc = getLocations(seeds[i], seeds[i + 1], listMaps);
            if (curLoc < lowestLoc) lowestLoc = curLoc;
        }

        _partTwo = lowestLoc.ToString();
    }
}
