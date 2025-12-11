using System.Text.RegularExpressions;
using Microsoft.Z3;

namespace Days.Day10;

internal readonly partial record struct Machine(long Target, List<long> Buttons, List<int> Joltages)
{
    public static Machine Parse(string line)
    {
        var match = MyRegex().Match(line);

        var targetPattern = match.Groups[1].Value;
        var buttonDefinitions = match.Groups[2].Value;
        var voltageConfig = match.Groups[3].Value;

        var totalLights = targetPattern.Length;
        var binaryTargetString = targetPattern.Replace('.', '0').Replace('#', '1');
        var target = Convert.ToInt64(binaryTargetString, 2);

        var buttons = buttonDefinitions.Split(' ').Select(bStr => {
            var cleanDefinition = bStr.Trim('(', ')');
            var activeBitsIndices = cleanDefinition.Split(',').Select(int.Parse);

            long buttonValue = 0;
            foreach (var bitIndex in activeBitsIndices)
            {
                buttonValue |= 1L << (totalLights - 1 - bitIndex);
            }
            return buttonValue;
        }).ToList();

        var joltages = voltageConfig.Split(',').Select(int.Parse).ToList();

        return new Machine(target, buttons, joltages);
    }
    
    public int CalculateMinimumLightPresses()
    {
        var queue = new Queue<(long pattern, int presses)>();
        var visited = new HashSet<long>();

        queue.Enqueue((0, 0));
        visited.Add(0);

        while (queue.Count > 0)
        {
            var (currentPattern, presses) = queue.Dequeue();

            if (currentPattern == Target)
            {
                return presses;
            }

            foreach (var buttonMask in Buttons)
            {
                var nextPattern = currentPattern ^ buttonMask;

                if (visited.Add(nextPattern))
                {
                    queue.Enqueue((nextPattern, presses + 1));
                }
            }
        }

        return 0; // Target unreachable
    }
    
    public long CalculateMinPressesZ3()
    {
        using var context = new Context();
        var optimizer = context.MkOptimize();

        var pressCountVars = new ArithExpr[Buttons.Count];
        for (var i = 0; i < Buttons.Count; i++)
        {
            pressCountVars[i] = context.MkIntConst($"btn_{i}");
            optimizer.Add(context.MkGe(pressCountVars[i], context.MkInt(0)));
        }

        for (var rowIndex = 0; rowIndex < Joltages.Count; rowIndex++)
        {
            var targetValue = Joltages[rowIndex];
            var terms = new List<ArithExpr>();

            for (var btnIndex = 0; btnIndex < Buttons.Count; btnIndex++)
            {
                var shiftAmount = Joltages.Count - 1 - rowIndex;
                var affectsRow = ((Buttons[btnIndex] >> shiftAmount) & 1) == 1;

                if (affectsRow)
                {
                    terms.Add(pressCountVars[btnIndex]);
                }
            }

            if (terms.Count == 0 && targetValue != 0) return 0;

            var sumOfTerms = context.MkAdd(terms.ToArray());
            optimizer.Add(context.MkEq(sumOfTerms, context.MkInt(targetValue)));
        }

        var totalPressesExpr = context.MkAdd(pressCountVars);
        var objective = optimizer.MkMinimize(totalPressesExpr);

        if (optimizer.Check() == Status.SATISFIABLE)
        {
            var resultExpr = objective.Value as IntNum;
            return resultExpr?.Int64 ?? 0;
        }

        return 0; // Unsolvable
    }

    [GeneratedRegex(@"\[(.*?)\] (.*?) \{(.*?)\}")]
    private static partial Regex MyRegex();
};