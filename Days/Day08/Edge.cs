namespace Days.Day08;

public struct Edge : IComparable<Edge>
{
    public int U;
    public int V;
    public long DistSq;

    public int CompareTo(Edge other)
    {
        return DistSq.CompareTo(other.DistSq);
    }
}