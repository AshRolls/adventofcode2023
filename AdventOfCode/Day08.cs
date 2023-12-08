using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode;

public class Day08 : BaseDay
{
    private readonly string[] _input;
    private string _partOne;
    private string _partTwo;

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        solve1();
        return new(_partOne);
    }

    private class Node
    {
        public string Name { get; set; }
        public Node L { get; set; }
        public Node R { get; set; }
    }


    private void solve1()
    {
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        foreach (String s in  _input.Skip(2)) 
        {
            Node n = new Node();
            n.Name = s.Substring(0, 3);
            nodes.Add(n.Name, n);
        }

        foreach (String s in _input.Skip(2))
        {
            Node n = nodes[s.Substring(0, 3)];
            n.L = nodes[s.Substring(7, 3)];
            n.R = nodes[s.Substring(12, 3)];
        }


        Node curNode = nodes["AAA"];
        int i = 0;
        int steps = 0;
        while (true)
        {            
            if (i == _input[0].Length)
                i = 0;
            if (_input[0][i] == 'L') curNode = curNode.L;
            else curNode = curNode.R;
            i++;
            steps++;
            if (curNode.Name == "ZZZ") break;
        }


        //HashSet<Node> unexploredNodes = new HashSet<Node>();

        //while (unexploredNodes.Count != 0)
        //{

        //}
        
        _partOne = steps.ToString();
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
