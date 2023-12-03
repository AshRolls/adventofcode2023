using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

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
    private List<(int, int)> _checkPos = new List<(int, int)>()
    {
        (-1,-1),(0,-1),(1,-1),
        (-1,0) ,       (1,0),
        (-1,1) ,(0,1) ,(1,1)
    };
    private void solve1()
    {
        char[,] engine = parseFile();
        int total = 0;
        
        for (int y = 0; y < _size; y++)
        {
            bool num = false;
            bool adjacent = false;
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < _size; x++)
            {
                if (Char.IsNumber(engine[x, y]))
                {
                    num = true;
                    sb.Append(engine[x, y]);
                    if (checkPositions(engine, x, y)) adjacent = true;
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

    private char[,] parseFile()
    {
        char[,] engine = new char[_size, _size];
        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                engine[x, y] = _input[y][x];
            }
        }

        return engine;
    }

    private bool checkPositions(char[,] engine, int x, int y)
    {
        foreach ((int, int) pos in _checkPos)
        {
            if ((x + pos.Item1 > 0 && x + pos.Item1 < _size) &&
                (y + pos.Item2 > 0 && y + pos.Item2 < _size))
            {
                if (engine[x + pos.Item1, y + pos.Item2] != '.' && !Char.IsNumber(engine[x + pos.Item1, y + pos.Item2]))
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
        char[,] engine = parseFile();
        int total = 0;        
        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                if (engine[x, y] == '*')
                {
                    if (tryGetNumbersFromPos(x, y, engine, out (int, int) gears))
                    {
                        total += gears.Item1 * gears.Item2;
                    }
                }
            }
        }

        _partTwo = total.ToString();
    }

    private bool tryGetNumbersFromPos(int x, int y, char[,] engine, out (int, int) gears)
    {
        List<int> numbers = new List<int>();
        foreach ((int, int) pos in _checkPos)
        {
            int checkPosX = x + pos.Item1;
            int checkPosY = y + pos.Item2;

            if ((checkPosX >= 0 && checkPosX < _size) &&
                (checkPosY >= 0 && checkPosY < _size))
            {
                int xOffset = 0;
                if (Char.IsNumber(engine[checkPosX, checkPosY]))
                {
                    // move left until start of number
                    while (checkPosX + xOffset >= 0 && Char.IsNumber(engine[checkPosX + xOffset - 1, checkPosY]))
                    {                        
                        xOffset--;
                        if (checkPosX + xOffset - 1 < 0) break;
                    }
                    StringBuilder sb = new StringBuilder();
                    int xPos = 0;                                        
                    while (Char.IsNumber(engine[checkPosX + xOffset + xPos, checkPosY]))
                    {
                        sb.Append(engine[checkPosX + xOffset + xPos, checkPosY]);
                        xPos++;
                        if (checkPosX + xOffset + xPos > _size - 1) break;
                    }
                    
                    Int32.TryParse(sb.ToString(), out int num);
                    sb.Clear();
                    numbers.Add(num);
                }
            }
        }
        List<int> noDuplicates = numbers.Distinct().ToList();
        if (noDuplicates.Count == 2)
        {
            gears = (noDuplicates[0], noDuplicates[1]);
            return true;
        }
        else
        {
            gears = (0, 0);
            return false;
        }
    } 
}
