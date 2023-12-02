namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day01()
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
        var detect = new Dictionary<string, int>()
        {
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "0", 0 }
        };

        char digit1 = ' ';
        char digit2 = ' ';
        int total = 0;
        foreach (string s in _input)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (detect.ContainsKey(s[i].ToString()))
                {
                    digit1 = s[i];
                    break;
                }
            }
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (detect.ContainsKey(s[i].ToString()))
                {
                    digit2 = s[i];
                    break;
                }
            }
            
            total += Int32.Parse(digit1.ToString()) * 10; 
            total += Int32.Parse(digit2.ToString());        
        }
        _partOne = total.ToString();
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

    
