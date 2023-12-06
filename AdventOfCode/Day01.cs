using System.Linq;

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
        int total = 0;
        foreach (string s in _input)
        {
            var nums = AoCHelper.GetSingleNumsFromStr(s);
            total += nums.First() * 10;
            total += nums.Last();
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
            { "0", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }, 
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }                           
        };

        char digit1 = ' ';
        char digit2 = ' ';
        int total = 0;
        foreach (string s in _input)
        {
            bool found = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (found) break;
                
                foreach (String key in detect.Keys)
                {
                    if (i + key.Length <= s.Length)
                    {
                        if (s.Substring(i, key.Length) == key)
                        {
                            digit1 = detect[key].ToString()[0];
                            found = true;
                            break;
                        }
                    }
                }                                         
            }
            
            found = false;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (found) break;

                foreach (String key in detect.Keys)
                {
                    if (i + key.Length <= s.Length)
                    {
                        if (s.Substring(i, key.Length) == key)
                        {
                            digit2 = detect[key].ToString()[0];
                            found = true;
                            break;
                        }
                    }
                }               
            }

            total += Int32.Parse(digit1.ToString()) * 10;
            total += Int32.Parse(digit2.ToString());
        }
        _partTwo = total.ToString();
    }
}

    
