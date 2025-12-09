namespace Days.Day07;

public class Solution : BaseSolution
{
    private static readonly string Input = File.ReadAllText("Input Files/07.txt");
    private static readonly string[] Lines = Input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
    
    
    public override object RunPart1()
    {
        var matrix = ToRectangularMatrix();
        var startingColumn = Lines[0].IndexOf('S');
        matrix[startingColumn, 0] = '|';

        var splitCount = 0;

        for (var y = 1; y < matrix.Height; y++)
        {
            for (var x = 0; x < matrix.Width; x++)
            {
                if (matrix[x, y - 1] != '|') continue;
                switch (matrix[x, y])
                {
                    case '.':
                        matrix[x, y] = '|';
                        break;
                    case '^':
                        matrix[x-1, y] = '|';
                        matrix[x+1, y] = '|';
                        splitCount++;
                        break;
                }
            }
            
        }
        
        return splitCount;
    }

    public override object RunPart2()
    {
        const char splitter = '^';
        const char startMarker = 'S';

        var terrainMap = ToRectangularMatrix();
        var pathCounts = new Matrix<long>(terrainMap.Width, terrainMap.Height);

        var entryColumn = Lines[0].IndexOf(startMarker);
        pathCounts[entryColumn, 0] = 1;

        for (var row = 0; row < terrainMap.Height - 1; row++)
        {
            for (var col = 0; col < terrainMap.Width; col++)
            {
                var currentPaths = pathCounts[col, row];

                if (currentPaths == 0) continue;

                var structureBelow = terrainMap[col, row + 1];

                if (structureBelow == splitter)
                {
                    pathCounts[col - 1, row + 1] += currentPaths;
                    pathCounts[col + 1, row + 1] += currentPaths;
                }
                else
                {
                    pathCounts[col, row + 1] += currentPaths;
                }
            }
        }

        var lastRowIndex = pathCounts.Height - 1;
    
        return pathCounts
            .Where(cell => cell.Y == lastRowIndex)
            .Sum(cell => cell.Value);
    }
    
    private static Matrix<char> ToRectangularMatrix()
    {
        return ToMatrix(character => character);
    }
    
    private static Matrix<T> ToMatrix<T>(Func<char, T> transformation)
    {
        var height = Lines.Length;
        var width = Lines[0].Length;
        var matrix = new Matrix<T>(width, height);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                matrix[x, y] = transformation(Lines[y][x]);
            }
        }

        return matrix;
    }
}