namespace Days;

public interface ISolution
{
    object RunPart1();
    object RunPart2();
}

public abstract class BaseSolution : ISolution
{
    public abstract object RunPart1();
    public abstract object RunPart2();
}