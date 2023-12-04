
namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day04()
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
        int total = 0;
        foreach (string s in  _input)
        {
            total += getPoints(AoCHelper.GetNumsFromStr(s));
        }
        _partOne = total.ToString();
    }

    private int getPoints(int[] ints)
    {
        HashSet<int> winning = new HashSet<int>();
        for (int i = 1; i < 11;  i++)
        {
            winning.Add(ints[i]);
        }

        List<int> nums = new List<int>();
        for (int i = 11; i < ints.Length; i++)
        {
            nums.Add(ints[i]);
        }

        int match = 0;
        foreach (int i in nums)
        {
            if (winning.Contains(i)) match++;
        }

        int points = 0;
        if (match > 0)
        {
            points = 1;
            for (int i = 0; i < match - 1; i++)
            {
                points *= 2;
            }
        }

        return points;
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
