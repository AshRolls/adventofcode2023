
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day06 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day06()
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
        int[] time = AoCHelper.GetNumsFromStr(_input[0]);
        int[] dist = AoCHelper.GetNumsFromStr(_input[1]);
        int[] waysToBeat = new int[time.Length];

        for (int i = 0; i < time.Length; i++)
        {      
            for (int t = 1; t <= time[i]; t++)
            {
                if (t * (time[i] - t) > dist[i]) waysToBeat[i]++;
            }
        }

        int prod = 1;
        foreach (int v in waysToBeat) 
        {
            prod *= v;
        }

        _partOne = prod.ToString();
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        long time = AoCHelper.GetLongNumsFromStr(AoCHelper.StripWhiteSpace(_input[0]))[0];
        long dist = AoCHelper.GetLongNumsFromStr(AoCHelper.StripWhiteSpace(_input[1]))[0];
        long waysToBeat = 0;

        for (long t = 1; t <= time; t++)
        {
            if (t * (time - t) > dist) waysToBeat++;
        }     

        _partTwo = waysToBeat.ToString();
    }
}
