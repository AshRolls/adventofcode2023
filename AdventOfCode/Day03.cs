using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day03()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    private const int _size = 140;

    private void solve1()
    {
        char[,] engine = new char[_size,_size];
        for (int y = 0; y < _size; y++)            
        {            
            for (int x = 0; x < _size; x++)
            {
                engine[x, y] = _input[x][y];
            }
        }

        List<(int, int)> checkPos = new List<(int, int)>();
        checkPos.Add((-1, -1));
        checkPos.Add((0, -1));
        checkPos.Add((1, -1));
        checkPos.Add((-1, 0));
        checkPos.Add((1, 0));
        checkPos.Add((-1, 1));
        checkPos.Add((0, 1));
        checkPos.Add((1, 1));

        int total = 0;
        for (int x = 0; x < _size; x++)            
        {
            bool num = false;
            bool adjacent = false;
            StringBuilder sb = new StringBuilder ();
            for (int y = 0; y < _size; y++)
            {
                if (Char.IsNumber(engine[x, y]))
                {
                    num = true;
                    sb.Append(engine[x, y]);
                    if (checkPositions(engine, checkPos, x, y)) adjacent = true;
                }
                else if (num == true)
                {                                       
                    if (adjacent)
                    {
                        Int32.TryParse(sb.ToString(), out int val);
                        total += val;
                        adjacent = false;                        
                    }
                    num = false;
                    sb.Clear();
                }
            }
            if (num == true)
            {
                if (adjacent)
                {
                    Int32.TryParse(sb.ToString(), out int val);
                    total += val;
                }
                sb.Clear();
            }
        }

        _partOne = total.ToString();
    }   

    private bool checkPositions(char[,] engine, List<(int,int)> checkPos, int x, int y)
    {
        foreach ((int,int) pos in checkPos)
        {
            if ((x + pos.Item1 > 0 && x + pos.Item1 < _size) &&
                (y + pos.Item2 > 0 && y + pos.Item2 < _size))
            {
                if (engine[x + pos.Item1,y + pos.Item2] != '.' && !Char.IsNumber(engine[x + pos.Item1, y + pos.Item2]))
                {
                    return true;
                }
            }
        }
        return false;
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
