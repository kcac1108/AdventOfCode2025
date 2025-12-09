using QuikGraph;

namespace Days.Day08;

public class Solution : BaseSolution
{
    private static readonly Input Input = new(File.ReadAllText("Input Files/08.txt"));

    private const int PairsToConnect = 1000;
     
    public override object RunPart1()
    {
        ParseData(Input, out var distanceByJunctionBoxPair, out var circuitByJunctionBox);

        foreach (var (junctionBoxPair, distance) in distanceByJunctionBoxPair.OrderBy(pair => pair.Value).Take(PairsToConnect))
        {
            if (circuitByJunctionBox[junctionBoxPair.JunctionBox1] != circuitByJunctionBox[junctionBoxPair.JunctionBox2])
            {
                var newCircuit = circuitByJunctionBox[junctionBoxPair.JunctionBox1];
                var oldCircuit = circuitByJunctionBox[junctionBoxPair.JunctionBox2];

                foreach (var junctionBox in oldCircuit)
                {
                    newCircuit.Add(junctionBox);
                    circuitByJunctionBox[junctionBox] = newCircuit;
                }
            }
        }

        return circuitByJunctionBox.Values
            .Distinct()
            .OrderByDescending(circuit => circuit.Count)
            .Take(3)
            .Aggregate(1, (result, circuit) => result * circuit.Count);
    }

    private static void ParseData(
        Input input,
        out Dictionary<(Position JunctionBox1, Position JunctionBox2), double> distanceByJunctionBoxPair,
        out Dictionary<Position, HashSet<Position>> circuitByJunctionBox)
    {
        var junctionBoxes = input.Lines
            .Select(line =>
            {
                var rawCoordinates = line.Split(',');
                return new Position(int.Parse(rawCoordinates[0]), int.Parse(rawCoordinates[1]), int.Parse(rawCoordinates[2]));
            })
            .ToList();

        distanceByJunctionBoxPair = [];
        foreach (var junctionBox in junctionBoxes)
        {
            foreach (var otherJunctionBox in junctionBoxes)
            {
                if (junctionBox != otherJunctionBox
                    && !distanceByJunctionBoxPair.ContainsKey((junctionBox, otherJunctionBox))
                    && !distanceByJunctionBoxPair.ContainsKey((otherJunctionBox, junctionBox)))
                {
                    var distance = junctionBox.GetDistanceTo(otherJunctionBox);
                    distanceByJunctionBoxPair.Add((junctionBox, otherJunctionBox), distance);
                }
            }
        }

        circuitByJunctionBox = junctionBoxes.ToDictionary(
            junctionBox => junctionBox,
            junctionBox => new HashSet<Position> { junctionBox });
    }

    public override object RunPart2()
    {
        var points = new List<Point>();
        for (var i = 0; i < Input.Lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(Input.Lines[i])) continue;
            
            var parts = Input.Lines[i].Split(',');
            points.Add(new Point
            {
                Id = i,
                X = long.Parse(parts[0]),
                Y = long.Parse(parts[1]),
                Z = long.Parse(parts[2])
            });
        }

        var edges = new List<Edge>();
        var n = points.Count;

        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var dx = points[i].X - points[j].X;
                var dy = points[i].Y - points[j].Y;
                var dz = points[i].Z - points[j].Z;
                
                var distSq = dx * dx + dy * dy + dz * dz;

                edges.Add(new Edge { U = i, V = j, DistSq = distSq });
            }
        }

        edges.Sort();

        var dsu = new Dsu(n);
        
        foreach (var edge in edges)
        {
            if (dsu.Union(edge.U, edge.V))
            {
                if (dsu.ComponentCount == 1)
                {
                    var x1 = points[edge.U].X;
                    var x2 = points[edge.V].X;
                    var result = x1 * x2;

                    return result;
                }
            }
        }
        return 0;
    }
}
