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

        public bool StartNode { get; set; } = false;
        public bool EndNode { get; set; } = false;

        public long RepeatsAfter { get; set; }    
        public long EndNodeOn { get; set; }
    }


    private void solve1()
    {
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        parseNodes(nodes);

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

        _partOne = steps.ToString();
    }

    private void parseNodes(Dictionary<string, Node> nodes)
    {
        foreach (String s in _input.Skip(2))
        {
            Node n = new Node();
            n.Name = s.Substring(0, 3);
            if (n.Name[2] == 'A') n.StartNode = true;
            if (n.Name[2] == 'Z') n.EndNode = true;
            nodes.Add(n.Name, n);
        }

        foreach (String s in _input.Skip(2))
        {
            Node n = nodes[s.Substring(0, 3)];
            n.L = nodes[s.Substring(7, 3)];
            n.R = nodes[s.Substring(12, 3)];
        }
    }

    public override ValueTask<string> Solve_2()
    {
        solve2();
        return new(_partTwo);
    }

    private void solve2()
    {
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        parseNodes(nodes);

        List<Node> simulNodes = nodes.Values.Where(x => x.StartNode).ToList();
        foreach (Node n in simulNodes)
        {
            Node curNode = n;
            HashSet<(Node,int)> visited = new HashSet<(Node,int)>();
            int steps = 0;
            int i = 0;
            while(!visited.Contains((curNode,i)))
            {
                if (curNode.EndNode)
                {
                    n.EndNodeOn = steps;
                    visited.Add((curNode, i));
                }

                if (i == _input[0].Length) i = 0;
                if (_input[0][i] == 'L') curNode = curNode.L;
                else curNode = curNode.R;
                i++;
                steps++;                
            }
            n.RepeatsAfter = steps - n.EndNodeOn;
        }

        _partTwo = AoCHelper.LeastCommonMultipleOfArray(simulNodes.Select(x => x.RepeatsAfter).ToArray(), 0).ToString();
    }
}
