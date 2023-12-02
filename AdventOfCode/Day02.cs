using Spectre.Console;

namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day02()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    internal class Game
    {
        private List<(int, int, int)> Views = new();        
        public int Id { get; set; }        
        public int RMax { get; private set; }
        public int GMax { get; private set; }
        public int BMax { get; private set; }

        public Game()
        {
            RMax = 0;
            GMax = 0;
            BMax = 0;
        }            

        public void AddView(ValueTuple<int,int,int> val)
        {
            if (val.Item1 > RMax) RMax = val.Item1;
            if (val.Item2 > GMax) GMax = val.Item2;
            if (val.Item3 > BMax) BMax = val.Item3;
            Views.Add(val);
        }
    }

    private void solve1()
    {
        List<Game> games = new List<Game>();

        foreach(String s in _input)
        {
            Game g = new Game();                 
            g.Id = AoCHelper.GetNumsFromStr(s).First();
            foreach (var a in s.Split(':').Last().Split(';'))
            {                
                int red = 0;
                int green = 0;
                int blue = 0;                
                var rbg = a.Split(',');                
                foreach (var b in rbg)
                {                
                    int qty = AoCHelper.GetNumsFromStr(b).First();                    
                    switch (b.Split(' ').Last())
                    {
                        case "red":
                            red = qty;
                            break;
                        case "green":
                            green = qty;
                            break;
                        case "blue":
                            blue = qty;
                            break;
                    }                                        
                }
                ValueTuple<int, int, int> vals = new ValueTuple<int, int, int>(red, green, blue);
                g.AddView(vals);
            }
            games.Add(g);
        }

        int total = 0;
        foreach (Game g in games)
        {
            if (g.RMax <= 12 && g.GMax <= 13 && g.BMax <= 14) total += g.Id;            
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
