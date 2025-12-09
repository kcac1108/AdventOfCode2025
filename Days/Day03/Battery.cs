namespace Days.Day03;

internal class Battery(char joltage, int indexInBank) : IComparable<Battery>
{
    public char RawJoltage => joltage;
    public int Joltage => RawJoltage - '0';
    public int IndexInBank => indexInBank;

    public int CompareTo(Battery? other)
    {
        return other is null ? 1 : Joltage.CompareTo(other.Joltage);
    }

    public override string ToString()
    {
        return $"#{indexInBank}: {joltage}";
    }
}