using System.Numerics;

namespace AdventOfCode;

public class Day10 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day10()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    private class Tile
    {
        public Tile A { get; set; }
        public Tile B { get; set; }
        public char C { get; set; }

        public int D { get; set; }
    }

    private void solve1()
    {
        Tile[,] grid = new Tile[142, 142];
        Tile start;
        (int x, int y) startXY;
        parseFile(grid, out start, out startXY);

        List<Tile> dirs = new List<Tile>();
        findDirections(grid, start, startXY, dirs);

        HashSet<Tile> visited = new HashSet<Tile>();
        _partOne = traverseLoop(start, dirs, 0, visited).ToString();
    }

    private static int traverseLoop(Tile start, List<Tile> dirs, int steps, HashSet<Tile> visited)
    {
        Tile lastTile0 = start;
        Tile lastTile1 = start;
        Tile nextTile0 = dirs[0];
        Tile nextTile1 = dirs[1];

        while (!visited.Contains(nextTile0) && !visited.Contains(nextTile1))
        {
            steps++;
            visited.Add(nextTile0);
            visited.Add(nextTile1);
            nextTile0.D = steps;
            nextTile1.D = steps;

            if (nextTile0.A != lastTile0)
            {
                lastTile0 = nextTile0;
                nextTile0 = nextTile0.A;
            }
            else
            {
                lastTile0 = nextTile0;
                nextTile0 = nextTile0.B;
            }

            if (nextTile1.A != lastTile1)
            {
                lastTile1 = nextTile1;
                nextTile1 = nextTile1.A;
            }
            else
            {
                lastTile1 = nextTile1;
                nextTile1 = nextTile1.B;
            }
        }

        return steps;
    }

    private static void findDirections(Tile[,] grid, Tile start, (int x, int y) startXY, List<Tile> dirs)
    {
        Tile up = grid[startXY.x, startXY.y - 1];
        Tile down = grid[startXY.x, startXY.y + 1];
        Tile left = grid[startXY.x - 1, startXY.y];
        Tile right = grid[startXY.x + 1, startXY.y];

        if (up.A == start || up.B == start) dirs.Add(up);
        if (down.A == start || down.B == start) dirs.Add(down);
        if (left.A == start || left.B == start) dirs.Add(left);
        if (right.A == start || right.B == start) dirs.Add(right);
    }

    private void parseFile(Tile[,] grid, out Tile start, out (int x, int y) startXY)
    {
        for (int y = 0; y <= 141; y++)
        {
            for (int x = 0; x <= 141; x++)
            {
                Tile t = new Tile();
                grid[x, y] = t;
            }
        }

        start = null;
        startXY = (0, 0);
        for (int y = 1; y < 141; y++)
        {
            for (int x = 1; x < 141; x++)
            {
                if (parseChar(grid, x, y, _input[y - 1][x - 1]))
                {
                    start = grid[x, y];
                    startXY = (x, y);
                }
            }
        }
    }

    private bool parseChar(Tile[,] grid, int x, int y, char c)
    {        
        Tile t = grid[x, y];
        switch (c)
        {
            case '.':
                t.C = '.';
                break;
            case '|':
                t.A = grid[x, y - 1];
                t.B = grid[x, y + 1];
                t.C = '|';
                break;
            case '-':
                t.A = grid[x + 1, y];
                t.B = grid[x - 1, y];
                t.C = '-';
                break;
            case 'L':
                t.A = grid[x, y - 1];
                t.B = grid[x + 1, y];
                t.C = 'L';
                break;
            case 'J':
                t.A = grid[x, y - 1];
                t.B = grid[x - 1, y];
                t.C = 'J';
                break;
            case '7':
                t.A = grid[x, y + 1];
                t.B = grid[x - 1, y];
                t.C = '7';
                break;
            case 'F':
                t.A = grid[x, y + 1];
                t.B = grid[x + 1, y];
                t.C = 'F';
                break;
            case 'S':
                t.C = 'S';
                return true;
                break;
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
        Tile[,] grid = new Tile[142, 142];
        Tile start;
        (int x, int y) startXY;
        parseFile(grid, out start, out startXY);

        List<Tile> dirs = new List<Tile>();
        findDirections(grid, start, startXY, dirs);

        HashSet<Tile> visited = new HashSet<Tile>();
        traverseLoop(start, dirs, 0, visited).ToString();
        visited.Add(start);

        int area = 0;
        for (int y = 1; y < 141; y++)
        {
            bool inside = false;
            for (int x = 1; x < 141; x++)
            {       
                if ( ((grid[x, y].C == '|') || (grid[x, y].C == '7') || (grid[x, y].C == 'F') || (grid[x, y].C == 'S')) && visited.Contains(grid[x, y])) inside = !inside;
                else if (inside && !visited.Contains(grid[x,y])) area++;
            }
        }

        _partTwo = area.ToString();
    }
}
