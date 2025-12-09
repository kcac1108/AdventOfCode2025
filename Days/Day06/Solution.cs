namespace Days.Day06;

public class Solution : BaseSolution
{
    private static readonly string Input = File.ReadAllText("Input Files/06.txt").Replace("\r", "");
    private static readonly string[] Lines = Input.Split('\n');
    
    public override object RunPart1()
    {
        var grid = Lines
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .ToArray();

        var results = new List<long>();

        var rowsLength = grid.Length;
        var colsLenght = grid[0].Length; 

        for (var col = 0; col < colsLenght; col++)
        {
            var op = grid[rowsLength - 1][col];

            var numbers = new List<long>();
            for (var row = 0; row < rowsLength - 1; row++)
            {
                if (long.TryParse(grid[row][col], out var num))
                {
                    numbers.Add(num);
                }
            }

            switch (op)
            {
                case "+":
                    results.Add(numbers.Sum());
                    break;
                case "*":
                    results.Add(numbers.Aggregate(1L, (acc, val) => acc * val));
                    break;
            }
        }

        return results.Sum(); 
    }

    public override object RunPart2()
    {
        var operatorRowIndex = Lines.Length - 1;
        var operatorRow = Lines[operatorRowIndex];

        var operatorIndices = GetOperatorColumnIndices(operatorRow);

        var gridRightEdge = operatorRow.Length + 1;
        operatorIndices.Add(gridRightEdge);

        long grandTotal = 0;

        for (var i = operatorIndices.Count - 1; i > 0; i--)
        {
            var currentOperatorIndex = operatorIndices[i - 1];
            var nextSectionStart = operatorIndices[i];

            var sectionRightBound = nextSectionStart - 2;
            var sectionLeftBound = currentOperatorIndex;

            var operationChar = operatorRow[currentOperatorIndex];

            var sectionTotal = CalculateSectionTotal(Lines, sectionLeftBound, sectionRightBound, operatorRowIndex, operationChar);
            
            grandTotal += sectionTotal;
        }

        return grandTotal;
    }

    private static long CalculateSectionTotal(string[] lines, int leftBound, int rightBound, int operatorRowIndex, char operation)
    {
        var total = (operation == '+') ? 0L : 1L;

        for (var col = rightBound; col >= leftBound; col--)
        {
            var number = ParseVerticalNumber(lines, col, operatorRowIndex);

            if (operation == '+')
            {
                total += number;
            }
            else
            {
                total *= number;
            }
        }

        return total;
    }

    private static int ParseVerticalNumber(string[] lines, int colIndex, int maxRowIndex)
    {
        var rawNumber = "";
        for (var row = 0; row < maxRowIndex; row++)
        {
            rawNumber += lines[row][colIndex];
        }
        
        return int.Parse(rawNumber.Trim());
    }

    private static List<int> GetOperatorColumnIndices(string operatorRow)
    {
        var indices = new List<int>();
        for (var i = 0; i < operatorRow.Length; i++)
        {
            if (operatorRow[i] == '+' || operatorRow[i] == '*')
            {
                indices.Add(i);
            }
        }
        return indices;
    }
}