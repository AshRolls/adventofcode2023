namespace AdventOfCode;

public class Day11 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day11()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    private class Galaxy
    {
        public int X {  get; set; }
        public int Y { get; set; }
    }

    private void solve1()
    {
        List<Galaxy> galaxies = new List<Galaxy>();
        List<int> rows = new List<int>();
        List<int> cols = new List<int>();
        int expansionFactor = 1;

        long sum = solve(galaxies, rows, cols, expansionFactor);

        _partOne = sum.ToString();
    }

    private long solve(List<Galaxy> galaxies, List<int> rows, List<int> cols, int expansionFactor)
    {
        for (int y = 0; y < _input.Length; y++)
        {
            bool noGalaxyRow = true;
            int x;
            for (x = 0; x < _input[y].Length; x++)
            {
                if (_input[y][x] == '#')
                {
                    galaxies.Add(new Galaxy { X = x, Y = y });
                    noGalaxyRow = false;
                }
            }
            if (noGalaxyRow) rows.Add(y);
        }

        for (int x = 0; x < _input[0].Length; x++)
        {
            bool noGalaxyCol = true;
            int y;
            for (y = 0; y < _input.Length; y++)
            {
                if (_input[y][x] == '#')
                {
                    noGalaxyCol = false;
                }
            }
            if (noGalaxyCol) cols.Add(x);
        }

        HashSet<Galaxy> pairedGals = new HashSet<Galaxy>();
        long sum = 0;
        int pairs = 0;
        for (int i = 0; i < galaxies.Count; i++)
        {
            pairedGals.Add(galaxies[i]);
            foreach (Galaxy g in galaxies.Where(x => !pairedGals.Contains(x)))
            {
                pairs++;
                int r = rows.Count(row => row < Math.Max(galaxies[i].Y, g.Y) && row > Math.Min(galaxies[i].Y, g.Y));
                int c = cols.Count(col => col < Math.Max(galaxies[i].X, g.X) && col > Math.Min(galaxies[i].X, g.X));
                sum += AoCHelper.GetManhattanDist(galaxies[i].X, galaxies[i].Y, g.X, g.Y) + r * expansionFactor + c * expansionFactor;
            }
        }

        return sum;
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        List<Galaxy> galaxies = new List<Galaxy>();
        List<int> rows = new List<int>();
        List<int> cols = new List<int>();
        int expansionFactor = 999999;

        long sum = solve(galaxies, rows, cols, expansionFactor);

        _partTwo = sum.ToString();
    }
}
