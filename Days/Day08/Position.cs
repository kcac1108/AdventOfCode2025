namespace Days.Day08;

internal readonly record struct Position(int X, int Y, int Z)
{
    public double GetDistanceTo(Position other)
    {
        return Math.Cbrt(Math.Pow(X - other.X, 2)
                         + Math.Pow(Y - other.Y, 2)
                         + Math.Pow(Z - other.Z, 2));
    }
}