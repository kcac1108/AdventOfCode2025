namespace Days.Day08;

public class Dsu
{
    private readonly int[] _parent;
    public int ComponentCount { get; private set; }

    public Dsu(int n)
    {
        _parent = new int[n];
        for (var i = 0; i < n; i++) _parent[i] = i;
        ComponentCount = n;
    }

    public int Find(int i)
    {
        if (_parent[i] != i)
            _parent[i] = Find(_parent[i]); // Path compression
        return _parent[i];
    }

    public bool Union(int i, int j)
    {
        var rootA = Find(i);
        var rootB = Find(j);

        if (rootA != rootB)
        {
            _parent[rootA] = rootB;
            ComponentCount--;
            return true; // A merge happened
        }
        return false; // Already connected
    }
}