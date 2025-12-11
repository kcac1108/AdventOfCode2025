namespace Days.Day10;

public class Solution : BaseSolution
{
    private static readonly Input Input = new(File.ReadAllText("Input Files/10.txt"));
    
    public override object RunPart1()
    {
        var machines = Input.Lines.Select(Machine.Parse).ToList();
        return machines.Sum(machine => machine.CalculateMinimumLightPresses());
    }

    public override object RunPart2()
    {
        var machines = Input.Lines.Select(Machine.Parse).ToList();
        return machines.Sum(machine => machine.CalculateMinPressesZ3());
    }
}