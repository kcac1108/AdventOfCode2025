using System.Collections;

namespace Days;

internal class Matrix<T>(int width, int height) : IEnumerable<(T Value, int X, int Y)>
{
    private readonly T[,] _values = new T[width, height];

    public int Width { get; } = width;
    public int Height { get; } = height;

    public T this[int x, int y]
    {
        get => _values[x, y];
        set => _values[x, y] = value;
    }

    public T? GetOrDefault(int x, int y)
    {
        if (!IsInside(x, y))
        {
            return default;
        }

        return _values[x, y];
    }

    public bool IsInside(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

    public IEnumerator<(T Value, int X, int Y)> GetEnumerator()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                yield return (_values[x, y], x, y);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}