namespace Days.Day04;

public class Solution : BaseSolution
{
    private static readonly string Input = File.ReadAllText("Input Files/04.txt");
    private static readonly string[] Lines = Input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    public override object RunPart1()
    {
        var grid = ToMatrix(character => character == '@');
        var accessibleRolls = grid.Where(tuple => tuple.Value && IsRollAccessible(grid, tuple.X, tuple.Y)).ToList();
        return accessibleRolls.Count;
    }

    public override object RunPart2()
    {
        var grid = ToMatrix(character => character == '@');
        var totalRemovedRolls = 0;
        int removedRolls;

        do
        {
            removedRolls = 0;
            var accessibleRolls = grid
                .Where(tuple => tuple.Value && IsRollAccessible(grid, tuple.X, tuple.Y))
                .ToList();
            
            foreach (var (_, x, y) in accessibleRolls)
            {
                grid[x, y] = false;
                removedRolls++;
                totalRemovedRolls++;
            }
        } while (removedRolls > 0);
        
        return totalRemovedRolls;
    }
    
    private static bool IsRollAccessible(Matrix<bool> grid, int rollX, int rollY)
    {
        var adjacentRolls = 0;

        for (var y = -1; y <= 1; y++)
        {
            for (var x = -1; x <= 1; x++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                if (grid.GetOrDefault(rollX + x, rollY + y))
                {
                    adjacentRolls++;
                }
            }
        }

        return adjacentRolls < 4;
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