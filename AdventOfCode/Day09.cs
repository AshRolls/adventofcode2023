

namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day09()
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
        int sum = 0;
        foreach (string s in  _input)
        {
            Stack<List<int>> diffs = new Stack<List<int>>();
            getDiffsStack(s, diffs);

            int newVal = 0;
            while (diffs.Count > 0)
            {
                newVal += diffs.Pop().Last();
            }
            sum += newVal;
        }
        _partOne = sum.ToString();
    }

    private void getDiffsStack(string s, Stack<List<int>> diffs)
    {
        var nums = AoCHelper.GetNumsFromStr(s);
        diffs.Push(nums.ToList());
        while (!diffs.Peek().All(x => x == 0))
        {
            diffs.Push(getDiffs(diffs.Peek()));
        }
    }

    private List<int> getDiffs(List<int> diffs)
    {
        List<int> nextDiffs = new List<int>();
        
        for (int i=0; i < diffs.Count - 1; i++)
        {
            nextDiffs.Add(diffs[i + 1] - diffs[i]);
        }
        return nextDiffs;
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        int sum = 0;
        foreach (string s in _input)
        {
            Stack<List<int>> diffs = new Stack<List<int>>();
            getDiffsStack(s, diffs);

            int newVal = 0;
            while (diffs.Count > 0)
            {
                newVal = diffs.Pop().First() - newVal;
            }
            sum += newVal;
        }
        _partTwo = sum.ToString();
    }
}
